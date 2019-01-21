using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskTrackerAPI.DAL.DAO;
using TaskTrackerAPI.DAL.Repositories;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.Controllers
{
    [Route("api/tasks")]
    public class CommentController : Controller
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentRepository _commentRepository;

        public CommentController(ILogger<CommentController> logger, ICommentRepository commentRepository)
        {
            _logger = logger;
            _commentRepository = commentRepository;

        }

        [HttpGet("{taskId}/comments")]
        public async Task<IActionResult> GetAllComments(int taskId)
        {
            if (taskId <= 0)
            {
                _logger.LogWarning("Incorect ID of task.");
                return BadRequest();
            }

            var comments = await _commentRepository.GetComments(taskId);
            return Ok(comments);
            
        }
               
        [HttpPost("{taskId}/comments")]
        public async Task<IActionResult> Create(int taskId, Comment comment)
        {
            if (taskId < 1)
            {
                return BadRequest("Error");
            }

            if (ModelState.IsValid)
            {
                await _commentRepository.AddComment(taskId, comment);
                return Ok();
            }

            return Ok();
        }
    
        [HttpDelete("{taskId}/comments/{commentId}")]
        public async Task<IActionResult> DeleteCom(int taskId, int commentId)
        {
            await _commentRepository.DeleteComment(taskId, commentId);
            return Ok();
        }
    }
}

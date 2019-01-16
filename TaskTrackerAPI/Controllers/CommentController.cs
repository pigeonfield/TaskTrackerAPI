using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskTrackerAPI.DAL.Repositories;

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
        public IActionResult GetComments(int taskId)
        {
            if (taskId <= 0)
            {
                _logger.LogWarning("Incorect ID of task.");
                return BadRequest();
            }

            return Ok(_commentRepository.GetComments(taskId));
            
        }


     }
}

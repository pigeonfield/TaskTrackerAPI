using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TaskTrackerAPI.Controllers
{
    [Route("api/tasks")]
    public class CommentController : Controller
    {
        [HttpGet("{taskId}/comments")]
        public IActionResult GetComments(int taskId)
        {
            var task = TemporaryDataStore.DummyData.Tasks.FirstOrDefault(t => t.TaskId == taskId);

            return Ok(task.Comments);
            
        }

        [HttpGet("{taskId}/comments/{id}")]
        public IActionResult GetComment(int taskId, int id)
        {
            var task = TemporaryDataStore.DummyData.Tasks.FirstOrDefault(t => t.TaskId == taskId);

            var comment = task.Comments.FirstOrDefault(c => c.CommentId == id);

            return Ok(comment);
        }


    }
}
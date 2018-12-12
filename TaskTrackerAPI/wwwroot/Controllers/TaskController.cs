using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TaskTrackerAPI.Controllers
{
    [Route("api/tasks")]
    public class TaskController : Controller
    {
        private ILogger<TaskController> _logger;

        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult GetAllTasks()
        {
            return Ok(TemporaryDataStore.DummyData.Tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int taskId)
        {
            var taskToReturn = TemporaryDataStore.DummyData.Tasks.FirstOrDefault(t => t.TaskId == taskId);
            if (taskToReturn == null)
            {
                _logger.LogWarning($"There is no task with {taskId} id.");
                return NotFound();
            }
            else
            {
                return Ok(taskToReturn);
            }
        }

        [HttpGet("categories/{categoryId}")]
        public IActionResult GetTasksByCategory(int categoryId)
        {


            var tasksByCategory = TemporaryDataStore.DummyData.Tasks.Where(t => t.CategoryId == categoryId);
            if (tasksByCategory.Count() == 0)
            {
                _logger.LogWarning($"There is no category with {categoryId} id.");
                return NotFound();
            }
            else
            {
                return Ok(tasksByCategory);
            }
            
        }

        [HttpGet("unfinished")]
        public IActionResult GetNotDoneTasks()
        {
            var notDoneTasks = TemporaryDataStore.DummyData.Tasks.Where(t => !t.IsDone);
            if (notDoneTasks.Count() == 0)
            {
                _logger.LogWarning($"There are no unfinished tasks.");
                return NoContent();
            }
            else
            {
                return Ok(notDoneTasks);
            }
            
        }

        [HttpGet("finished")]
        public IActionResult GetDoneTasks()
        {
            var doneTasks = TemporaryDataStore.DummyData.Tasks.Where(t => t.IsDone);
            if (doneTasks.Count() == 0)
            {
                _logger.LogWarning($"There are no finished tasks.");
                return NoContent();
            }
            else
            {
                return Ok(doneTasks);
            }
        }




    }
}
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
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskRepository _taskRepository; 

        public TaskController(ILogger<TaskController> logger, ITaskRepository taskRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }

        [HttpGet()]
        public IActionResult GetAllTasks()
        {
            return Ok(_taskRepository);
        }

        [HttpGet("{id}")]
        public IActionResult ShowTask(int taskId)
        {
            if (taskId <= 0)
            {
                _logger.LogError("Incorect ID of task.");
                return BadRequest();
            }

            var taskToReturn = _taskRepository.GetTask(taskId);
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
        public IActionResult ShowTasksByCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                _logger.LogError("Incorect ID of category.");
                return BadRequest();
            }

            var tasksByCategory = _taskRepository.GetTasksByCategory(categoryId);
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
        public IActionResult Unfinished()
        {
            var notDoneTasks = _taskRepository.GetNotDoneTasks();
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
        public IActionResult Finished()
        {
            var doneTasks = _taskRepository.GetDoneTasks();
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskTrackerAPI.DAL.Repositories;
using TaskTrackerAPI.DataFilters;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.Controllers
{
    [Route("api/tasks")]
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskRepository _taskRepository;
        private readonly LifetimeTest _lifetimeTest;

        public TaskController(ILogger<TaskController> logger, ITaskRepository taskRepository, LifetimeTest lifetimeTest)
        {
            _logger = logger;
            _taskRepository = taskRepository;
            _lifetimeTest = lifetimeTest;
        }

        [HttpGet("[action]")]
        public IActionResult GetTimestamp([FromServices] LifetimeTest lifetimeTestAction)
        {
            var ret = $"Controller di: {_lifetimeTest.Timestamp.Ticks}, BLL di: {lifetimeTestAction.Timestamp.Ticks}";

            return new ObjectResult(ret);
        }


        [HttpGet("{taskId?}")]
        public IActionResult ShowTask(int? taskId)
        {
            if (!taskId.HasValue)
            {
                return Ok(_taskRepository.GetAllTasks());
            }
            if (taskId <= 0)
            {
                _logger.LogError("Incorect ID of task.");
                return BadRequest();
            }

            var taskToReturn = _taskRepository.GetTask(taskId.Value);
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


        [HttpGet("[action]/{filter?}")]
        public IActionResult GetFilteredTasks([FromQuery] TaskFilter filter)
        {
            if (filter.IsEmpty)
            {
                return Ok(_taskRepository.GetAllTasks());
            }

            else
            {
                return Ok(_taskRepository.GetFilteredResult(filter));
            }
        }


        [HttpPost("")

    }
}
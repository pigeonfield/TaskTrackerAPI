using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskTrackerAPI.DAL.DAO;
using TaskTrackerAPI.DAL.Repositories;
using TaskTrackerAPI.DataFilters;
using TaskTrackerAPI.Models;
using TaskTrackerAPI.DAL.ExtensionMethods;


namespace TaskTrackerAPI.Controllers
{
    [Route("api/tasks")]
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskRepository _taskRepository;
        private readonly LifetimeTest _lifetimeTest;
        private readonly IMapper _mapper;

        public TaskController(ILogger<TaskController> logger, ITaskRepository taskRepository, LifetimeTest lifetimeTest, IMapper mapper)
        {
            _logger = logger;
            _taskRepository = taskRepository;
            _lifetimeTest = lifetimeTest;
            _mapper = mapper;
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
                return Ok(_taskRepository.GetAllTasks().Select(t => t.ConvertTaskToShowAllView()).ToList());
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

        [HttpPost]
        public IActionResult Create([FromBody]TaskModelCreate task)
        {
            if (ModelState.IsValid)
            {
                TaskModel taskToSave = task.ConvertTaskWhenCreate();
                _taskRepository.AddTask(taskToSave);

                return Ok(task);
            }
            else
            {
                _logger.LogWarning("Wrong type of input.");
                return BadRequest();
            }

        }

        [HttpPut]
        public IActionResult Update([FromBody]TaskModelUpdate task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskModel taskToUpdate = _taskRepository.GetTask(task.TaskId);

            if (taskToUpdate == null)
            {
                return NotFound();
            }

            _taskRepository.UpdateTask(taskToUpdate, task);   
            
            return Ok();
        }
        [HttpPut("{taskId}")]
        public IActionResult MarkTaskAsDone(int taskId)
        {
             _taskRepository.TaskIsDone(taskId);
             return Ok();
        }


        [HttpDelete("{taskId}")]
        public IActionResult Delete(int taskId)
        {
             _taskRepository.DeleteTask(taskId);
            return Ok();
        }

    }

    
}
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


        public TaskController(ILogger<TaskController> logger, ITaskRepository taskRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;

        }
        

        [HttpGet("{taskId?}")]
        public async Task<IActionResult> ShowTask(int? taskId)
        {
            if (!taskId.HasValue)
            {
                var nofilter = await _taskRepository.GetAllTasks();
                var nofilterSelect = nofilter.Select(t => t.ConvertTaskToShowAllView()).ToList();
                return Ok(nofilterSelect);
            }
            if (taskId <= 0)
            {
                _logger.LogError("Incorect ID of task.");
                return BadRequest();
            }

            var taskToReturn = await _taskRepository.GetTask(taskId.Value);
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
        public async Task<IActionResult> GetFilteredTasks([FromQuery] TaskFilter filter)
        {
            if (filter.IsEmpty)
            {
                //return Ok(_taskRepository.GetAllTasks()); 

                var all = await _taskRepository.GetAllTasks();
                return Ok(all);
            }
            else
            {
                //return Ok(_taskRepository.GetFilteredResult(filter));

                var filtered = await _taskRepository.GetFilteredResult(filter);
                return Ok(filtered);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]TaskModelCreate task)
        {
            if (ModelState.IsValid)
            {
                TaskModel taskToSave = task.ConvertTaskWhenCreate();
                await _taskRepository.AddTask(taskToSave);

                return Ok(task);
            }
            else
            {
                _logger.LogWarning("Wrong type of input.");
                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]TaskModelUpdate task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskModel taskToUpdate = await _taskRepository.GetTask(task.TaskId);

            if (taskToUpdate == null)
            {
                return NotFound();
            }

            TaskModel taskUpdated = task.ConvertTaskWhenUpdate();

            await _taskRepository.UpdateTask(taskToUpdate, taskUpdated);
                      

            return Ok();
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> MarkTaskAsDone(int taskId)
        {
             await _taskRepository.TaskIsDone(taskId);
             return Ok();
        }
            

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> Delete(int taskId)
        {
            await _taskRepository.DeleteTask(taskId);
            return Ok();
        }

    }

    
}
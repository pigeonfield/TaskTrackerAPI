using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DAL.DAO;
using TaskTrackerAPI.DataFilters;
using TaskTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;
using TaskTrackerAPI.DAL.ExtensionMethods;
using TaskTrackerAPI.DAL.DAO.Enums;

namespace TaskTrackerAPI.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _appDbContext;   

        public TaskRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<TaskModel> GetAllTasks()
        {
            return _appDbContext.Tasks;
        }

        public TaskModel GetTask(int taskId)
        {
            return _appDbContext.Tasks.Include(t => t.Comments).FirstOrDefault(t => t.TaskId == taskId);
        }



        public IEnumerable<TaskModel> GetFilteredResult([FromQuery] TaskFilter filter)
        {
            var isModificationRequired = true;
            var queryBuilder = isModificationRequired ? _appDbContext.Tasks : _appDbContext.Tasks.AsNoTracking();

            if (filter.Category.HasValue)
            {
                queryBuilder = queryBuilder.Where(task => task.Category == filter.Category);
            }
            if (filter.IsDone.HasValue)
            {
                queryBuilder = queryBuilder.Where(task => task.IsDone == filter.IsDone.Value);
            }

            return queryBuilder; 
        }

        public TaskModel AddTask(TaskModel task)
        {
            if (task != null)
            {
                if (task.CreatedAt == null)
                {
                    task.CreatedAt = DateTime.Now;
                }

                _appDbContext.Tasks.Add(task);
                _appDbContext.SaveChanges();
            }
             
            return task;
        }
        //taskOld - object from db, to be edited
        //taskNew - input 
        
        public TaskModel UpdateTask(TaskModel taskOld, TaskModelUpdate taskNew)
        {
            
            if (taskOld != null && taskNew != null)
            {
                TaskModel taskUpdated = taskNew.ConvertTaskWhenUpdate();

                if (!string.IsNullOrEmpty(taskUpdated.Name)) taskOld.Name = taskUpdated.Name;
                if (!string.IsNullOrEmpty(taskUpdated.ShortDescription)) taskOld.ShortDescription = taskUpdated.ShortDescription;
                if (taskUpdated.LongDescription != null) taskOld.LongDescription = taskUpdated.LongDescription;  // LongDesc exist
                    //if ((taskUpdated.LongDescription).Length == 0) taskOld.LongDescription = String.Empty;                          //LongDesc is empty string
                                                                                                                                    //LongDesc is null - do nothing
                taskOld.Priority = taskUpdated.Priority;
                taskOld.Category = taskUpdated.Category;

                _appDbContext.SaveChanges();
            }
       
            return taskOld;

        }

        public TaskModel TaskIsDone(int taskId)
        {
            TaskModel taskDone = _appDbContext.Tasks.FirstOrDefault(t => t.TaskId == taskId);

            if (taskDone != null)
            {
                taskDone.IsDone = true;
                _appDbContext.SaveChanges();
            }

            return taskDone;
        }

        public TaskModel DeleteTask(int taskId)
        {
            TaskModel taskToDelete = _appDbContext.Tasks.FirstOrDefault(t => t.TaskId == taskId);

            if (taskToDelete != null)
            {
                _appDbContext.Tasks.RemoveRange(taskToDelete);
                _appDbContext.SaveChanges();
            }

            return null;
        }


    }

}

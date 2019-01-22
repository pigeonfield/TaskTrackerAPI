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

        public async Task<IEnumerable<TaskModel>> GetAllTasks()
        {
            return await _appDbContext.Tasks.Include(t=>t.Comments).ToListAsync();
        }

        public async Task<TaskModel> GetTask(int taskId)
        {
            return await _appDbContext.Tasks.Include(t => t.Comments).SingleOrDefaultAsync(t => t.TaskId == taskId);
        }
        

        public async Task<IEnumerable<TaskModel>> GetFilteredResult([FromQuery] TaskFilter filter)
        {
            var isModificationRequired = true;
            var queryBuilder =  isModificationRequired ?  _appDbContext.Tasks :  _appDbContext.Tasks.AsNoTracking();

            if (filter.Category.HasValue)
            {
                queryBuilder = queryBuilder.Where(task => task.Category == filter.Category);
            }
            if (filter.IsDone.HasValue)
            {
                queryBuilder = queryBuilder.Where(task => task.IsDone == filter.IsDone.Value);
            }

            return await queryBuilder.ToListAsync(); 
        }

        public async Task AddTask(TaskModel task)
        {
            if (task != null)
            {
                if (task.CreatedAt == null)
                {
                    task.CreatedAt = DateTime.Now;
                }

                await _appDbContext.Tasks.AddAsync(task);
                await _appDbContext.SaveChangesAsync();
            }

        }
        //taskOld - object from db, to be edited
        //taskNew - input 
        
        public async Task UpdateTask(TaskModel taskOld, TaskModel taskNew)
        {
            
            if (taskOld != null && taskNew != null)
            {
                
                if (!string.IsNullOrEmpty(taskNew.Name)) taskOld.Name = taskNew.Name;
                if (!string.IsNullOrEmpty(taskNew.ShortDescription)) taskOld.ShortDescription = taskNew.ShortDescription;
                if (taskNew.LongDescription != null) taskOld.LongDescription = taskNew.LongDescription;  // LongDesc exist
                    //if ((taskUpdated.LongDescription).Length == 0) taskOld.LongDescription = String.Empty;                          //LongDesc is empty string
                                                                                                                                    //LongDesc is null - do nothing
                taskOld.Priority = taskNew.Priority;
                taskOld.Category = taskNew.Category;

                await _appDbContext.SaveChangesAsync();
            }
       
            
        }

        public async Task TaskIsDone(int taskId)
        {
            TaskModel taskDone = await _appDbContext.Tasks.SingleOrDefaultAsync(t => t.TaskId == taskId);

            if (taskDone != null)
            {
                taskDone.IsDone = true;
                await _appDbContext.SaveChangesAsync();
            }

        }

        public async Task DeleteTask(int taskId)
        {
            TaskModel taskToDelete = await _appDbContext.Tasks.SingleOrDefaultAsync(t => t.TaskId == taskId);

            if (taskToDelete != null)
            {
                _appDbContext.Tasks.RemoveRange(taskToDelete);
                await _appDbContext.SaveChangesAsync();
            }
            
        }


    }

}

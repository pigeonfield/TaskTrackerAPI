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

        public Task<List<TaskModel>> GetAllTasks()
        {
            //var allTasks = await _appDbContext.Tasks.AsNoTracking().ToListAsync();
            //allTasks.ForEach(task => 
            //    task.Comments = new List<Comment>(_appDbContext.Comments.Count(
            //        comment => comment.TaskId == task.TaskId)));

            return _appDbContext.Tasks.AsNoTracking().Include(t=>t.Comments).ToListAsync();
        }

        public Task<TaskModel> GetTask(int taskId)
        {
            return _appDbContext.Tasks.AsNoTracking().Include(t => t.Comments).FirstOrDefaultAsync(t => t.TaskId == taskId);
        }
        

        public Task<List<TaskModel>> GetFilteredResult([FromQuery] TaskFilter filter)
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

            return queryBuilder.ToListAsync(); 
        }

        public async Task AddTask(TaskModel task)
        {
            if (task != null)
            {
                if (task.CreatedAt == null)
                {
                    task.CreatedAt = DateTime.Now;
                }

                _appDbContext.Tasks.Add(task);
                await _appDbContext.SaveChangesAsync();
            }

        }
        
        public Task UpdateTask(TaskModel taskOld, TaskModel taskNew)
        {
            
            if (taskOld != null && taskNew != null)
            {
                
                if (!string.IsNullOrEmpty(taskNew.Name)) taskOld.Name = taskNew.Name;
                if (!string.IsNullOrEmpty(taskNew.ShortDescription)) taskOld.ShortDescription = taskNew.ShortDescription;
                if (taskNew.LongDescription != null) taskOld.LongDescription = taskNew.LongDescription;  

                taskOld.Priority = taskNew.Priority;
                taskOld.Category = taskNew.Category;
                                
            }

            return _appDbContext.SaveChangesAsync();     //?    
        }

        public Task TaskIsDone(int taskId)
        {
            TaskModel taskDone = _appDbContext.Tasks.FirstOrDefault(t => t.TaskId == taskId);

            if (taskDone != null)
            {
                taskDone.IsDone = true;
                
            }

            return _appDbContext.SaveChangesAsync();     //?
        }

        public Task DeleteTask(int taskId)
        {
            TaskModel taskToDelete = _appDbContext.Tasks.FirstOrDefault(t => t.TaskId == taskId);

            if (taskToDelete != null)
            {
                _appDbContext.Tasks.RemoveRange(taskToDelete);
                
            }
            
            return _appDbContext.SaveChangesAsync();     //?
        }


        #region Comments


        public Task<List<Comment>> GetComments(int taskId)
        {
            return _appDbContext.Comments.AsNoTracking().Where(t => t.TaskId == taskId).ToListAsync();            
        }

        public Task AddComment(int taskId, Comment comment)
        {
            TaskModel task = _appDbContext.Tasks.Include(c => c.Comments).FirstOrDefault(t => t.TaskId == taskId);

            if (task != null)
            {
                comment.TaskId = taskId;
                _appDbContext.Comments.AddRange(comment);
                
            }

            return _appDbContext.SaveChangesAsync();     //?
        }

        public Task DeleteComment(int taskId, int commentId)
        {
            TaskModel task = _appDbContext.Tasks.Include(c => c.Comments).FirstOrDefault(t => t.TaskId == taskId);

            if (task != null)
            {
                Comment comment = _appDbContext.Comments.FirstOrDefault(c => c.CommentId == commentId);
                _appDbContext.Comments.RemoveRange(comment);
                
            }

            return _appDbContext.SaveChangesAsync();     //?
        }

        #endregion

    }

}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DAL.DAO;
using TaskTrackerAPI.DataFilters;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.DAL.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>> GetAllTasks();

        Task<TaskModel> GetTask(int taskId);
                
        Task<List<TaskModel>> GetFilteredResult(TaskFilter filter);

        Task AddTask(TaskModel task);

        Task UpdateTask(TaskModel taskOld, TaskModel taskNew);

        Task TaskIsDone(int taskId);

        Task DeleteTask(int taskId);


        Task<List<Comment>> GetComments(int taskId);

        Task AddComment(int taskId, Comment comment);

        Task DeleteComment(int taskId, int commentId);

    }
}

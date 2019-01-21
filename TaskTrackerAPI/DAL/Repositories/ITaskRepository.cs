using System;
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
        Task<IEnumerable<TaskModel>> GetAllTasks();

        Task<TaskModel> GetTask(int taskId);
                
        Task<IEnumerable<TaskModel>> GetFilteredResult(TaskFilter filter);

        Task AddTask(TaskModel task);

        Task UpdateTask(TaskModel taskOld, TaskModel taskNew);

        Task TaskIsDone(int taskId);

        Task DeleteTask(int taskId);

       
    }
}

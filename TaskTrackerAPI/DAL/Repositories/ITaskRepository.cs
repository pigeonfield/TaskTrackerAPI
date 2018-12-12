using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.DAL.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TaskModel> GetAllTasks();

        TaskModel GetTask(int taskId);

        IEnumerable<TaskModel> GetTasksByCategory(int categoryId);

        IEnumerable<TaskModel> GetNotDoneTasks();

        IEnumerable<TaskModel> GetDoneTasks();

    }
}

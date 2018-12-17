using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DataFilters;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.DAL.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TaskModel> GetAllTasks();

        TaskModel GetTask(int taskId);



        IEnumerable<TaskModel> GetFilteredResult(TaskFilter filter);

    }
}

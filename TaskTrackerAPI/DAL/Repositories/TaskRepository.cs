using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public IEnumerable<TaskModel> GetAllTasks()
        {
            return TemporaryDataStore.DummyData.Tasks;
        }

        public TaskModel GetTask(int taskId)
        {
            return TemporaryDataStore.DummyData.Tasks.FirstOrDefault(t => t.TaskId == taskId);
        }

        public IEnumerable<TaskModel> GetTasksByCategory(int categoryId)
        {
            return TemporaryDataStore.DummyData.Tasks.Where(t => t.CategoryId == categoryId);
        }

        public IEnumerable<TaskModel> GetNotDoneTasks()
        {
            return TemporaryDataStore.DummyData.Tasks.Where(t => !t.IsDone);
        }

        public IEnumerable<TaskModel> GetDoneTasks()
        {
            return TemporaryDataStore.DummyData.Tasks.Where(t => t.IsDone);
        }
    }

}


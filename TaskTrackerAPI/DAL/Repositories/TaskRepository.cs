using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DataFilters;
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



        public IEnumerable<TaskModel> GetFilteredResult([FromQuery] TaskFilter filter)
        {
            if (filter.CategoryId == null)
            {
                return TemporaryDataStore.DummyData.Tasks.Where(t => t.IsDone == filter.IsDone);
            }
            else
            {
                if (filter.IsDone == null)
                {
                    return TemporaryDataStore.DummyData.Tasks.Where(t => t.CategoryId == filter.CategoryId);
                }
                else
                {
                    return TemporaryDataStore.DummyData.Tasks.Where(t => (t.CategoryId == filter.CategoryId && t.IsDone == filter.IsDone));   
                }
            }


        }
    }

}


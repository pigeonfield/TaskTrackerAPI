using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DAL.DAO;
using TaskTrackerAPI.DAL.DAO.Enums;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.DAL.ExtensionMethods
{
    public static class TaskModelConverter
    {
        public static TaskModel ConvertTaskWhenCreate(this TaskModelCreate taskModel)
        {
            return new TaskModel
            {
                Name = taskModel.Name,
                ShortDescription = taskModel.ShortDescription,
                LongDescription = taskModel.LongDescription,
                CreatedAt = DateTime.Now,
                Priority = (PriorityEnum)taskModel.Priority,
                IsDone = false,
                Category = (CategoryEnum)taskModel.Category
            };
        }

        public static TaskModelShowAll ConvertTaskToShowAllView(this TaskModel taskModel)
        {
            return new TaskModelShowAll
            {
                Name = taskModel.Name,
                ShortDescription = taskModel.ShortDescription,
                CreatedAt = taskModel.CreatedAt,
                Priority = taskModel.Priority,
                IsDone = taskModel.IsDone,
                Category = taskModel.Category,
                NumberOfComments = taskModel.Comments.ToList().Capacity
            };
        }

        public static TaskModel ConvertTaskWhenUpdate(this TaskModelUpdate taskModel)
        {
            return new TaskModel
            {
                Name = taskModel.Name,
                ShortDescription = taskModel.ShortDescription,
                LongDescription = taskModel.LongDescription,
                Priority = (PriorityEnum)taskModel.Priority,
                Category = (CategoryEnum)taskModel.Category
            };
        }
        //public static IEnumerable<TaskModelShowAll> ConvertTasksToShowAllView(this IEnumerable<TaskModel> taskModels) 
        //    => taskModels.Select(ts => ts.ConvertTaskToShowAllView());

    }
}

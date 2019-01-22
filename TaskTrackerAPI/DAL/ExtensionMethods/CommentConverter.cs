using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DAL.DAO;

namespace TaskTrackerAPI.DAL.ExtensionMethods
{
    public static class CommentConverter
    {
        public static Comment ConvertCommentWhenCreate(this CommentCreate commentCreated)
        {
            return new Comment
            {
                Content = commentCreated.Content,
                TaskId = commentCreated.TaskId
            };
        }
    }
}


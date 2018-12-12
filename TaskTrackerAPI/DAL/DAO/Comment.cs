using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.DAL.DAO
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string Content { get; set; }

        public virtual TaskModel Task { get; set; }
        public int TaskId { get; set; }

    }
}

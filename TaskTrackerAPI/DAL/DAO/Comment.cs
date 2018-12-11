using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTrackerAPI.DAL.DAO
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string Message { get; set; }

        public Task Task { get; set; }
        public int TaskId { get; set; }

    }
}

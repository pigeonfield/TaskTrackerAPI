using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DAL.DAO;

namespace TaskTrackerAPI.DAL.Repositories
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetComments(int taskId);
    }
}


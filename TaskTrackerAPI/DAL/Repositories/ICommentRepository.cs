using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DAL.DAO;

namespace TaskTrackerAPI.DAL.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetComments(int taskId);
        Task AddComment(int taskId, Comment comment);
        Task DeleteComment(int taskId, int commentId);
        
    }
}


using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DAL.DAO;

namespace TaskTrackerAPI.DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _appDbContext;

        public CommentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

       
        public IEnumerable<Comment> GetComments(int taskId)
        {
            return _appDbContext.Comments.Where(t => t.TaskId == taskId);
            
        }


    }
}

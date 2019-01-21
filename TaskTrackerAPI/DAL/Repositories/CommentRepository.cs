using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DAL.DAO;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _appDbContext;

        public CommentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<IEnumerable<Comment>> GetComments(int taskId)
        {
            var allComments = _appDbContext.Comments.Where(t => t.TaskId == taskId);
            return await allComments.ToListAsync();
        }

        public async Task AddComment(int taskId, Comment comment)
        {
            TaskModel task = await _appDbContext.Tasks.Include(c => c.Comments).SingleOrDefaultAsync(t => t.TaskId == taskId);

            if (task != null)
            {
                comment.TaskId = taskId;
                _appDbContext.Comments.AddRange(comment);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteComment(int taskId, int commentId)
        {
            TaskModel task = await _appDbContext.Tasks.Include(c=>c.Comments).SingleOrDefaultAsync(t => t.TaskId == taskId);

            if (task != null)
            {
                Comment comment = _appDbContext.Comments.FirstOrDefault(c => c.CommentId == commentId);
                _appDbContext.Comments.RemoveRange(comment);
                await _appDbContext.SaveChangesAsync();
            }

        }


    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.DAL.DAO
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
           
        }

        public DbSet<TaskModel> Tasks { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}

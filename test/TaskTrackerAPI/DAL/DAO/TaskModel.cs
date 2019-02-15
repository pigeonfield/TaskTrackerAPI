using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DAL.DAO;
using TaskTrackerAPI.DAL.DAO.Enums;

namespace TaskTrackerAPI.Models
{
    public class TaskModel
    {
        [Key]
        public int TaskId { get; set; }
        
        public string Name { get; set; }
        
        public string ShortDescription { get; set; }
        
        public string LongDescription { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public PriorityEnum Priority { get; set; }
        
        public bool IsDone { get; set; }
        
        public CategoryEnum Category { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
       
        
    }
}

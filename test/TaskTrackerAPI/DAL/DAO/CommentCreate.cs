using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTrackerAPI.DAL.DAO
{
    public class CommentCreate
    {
        [Required]
        [MaxLength(200)]
        public string Content { get; set; }
    
        public int TaskId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DAL.DAO.Enums;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.DAL.DAO
{

    public class TaskModelUpdate
    {
        [Required]
        public int TaskId { get; set; }
                
        [MaxLength(20)]
        public string Name { get; set; }
                
        [MaxLength(50)]
        public string ShortDescription { get; set; }

        [MaxLength(200)]
        public string LongDescription { get; set; }

        [Required]
        [Range(1, 3)]
        public int Priority { get; set; }

        [Required]
        [Range(1, 4)]
        public int Category { get; set; }
        
    }

}

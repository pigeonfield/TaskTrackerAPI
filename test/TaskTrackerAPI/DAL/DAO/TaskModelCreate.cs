using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DAL.DAO.Enums;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.DAL.DAO
{

    public class TaskModelCreate
    {
       
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string ShortDescription { get; set; }

        [MaxLength(200)]
        public string LongDescription { get; set; }
              
        [Required]
        [Range(1,3)]
        public int Priority { get; set; }

        public bool IsDone { get; set; }
      
        [Required]
        public int Category { get; set; }


    }

}

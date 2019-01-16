using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DAL.DAO.Enums;

namespace TaskTrackerAPI.DAL.DAO
{
    public class TaskModelShowAll
    {
 
        public string Name { get; set; }
        
        public string ShortDescription { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public PriorityEnum Priority { get; set; }
        
        public bool IsDone { get; set; }
    
        public CategoryEnum Category { get; set; }
   
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DAL.DAO.Enums;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.DAL.DAO
{
    
        public class TaskModelCreate
        {
            
            public string Name { get; set; }

            public string ShortDescription { get; set; }

            public string LongDescription { get; set; }

            public string Date { get; set; }

            public int Priority { get; set; }

            public bool IsDone { get; set; }


            public Category Category { get; set; }
            public int CategoryId { get; set; }

            public ICollection<Comment> Comments { get; set; }

        }
    
}

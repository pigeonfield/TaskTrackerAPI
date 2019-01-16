using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DAL.DAO.Enums;

namespace TaskTrackerAPI.DataFilters
{
    public class TaskFilter
    {
        public bool? IsDone { get; set; }
        
        public CategoryEnum? Category { get; set; }

        public bool IsEmpty
        {   get 
            {
                 return (IsDone == null && Category == null);
            }
           
        }

    }
}

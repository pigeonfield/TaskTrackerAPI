using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTrackerAPI.DataFilters
{
    public class TaskFilter
    {
        public bool? IsDone { get; set; }
        
        public int? CategoryId { get; set; }

        public bool IsEmpty
        {   get 
            {
                //default (int?);
                return (IsDone == null && CategoryId == null);
            }
           
        }

    }
}

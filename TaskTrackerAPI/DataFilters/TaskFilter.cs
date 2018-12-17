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
        
        public Category? Category { get; set; }

        public bool IsEmpty
        {   get 
            {
                //default (int?);
                return (IsDone == null && Category == null);
            }
           
        }

    }
}

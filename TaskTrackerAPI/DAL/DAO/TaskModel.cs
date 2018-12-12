using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DAL.DAO;

namespace TaskTrackerAPI.Models
{
    public class TaskModel
    {
        public int TaskId { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public DateTime Date { get; set; }

        public int Priority { get; set; }

        public bool IsDone { get; set; }


        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }

        public ICollection<Comment> Comments { get; set; }
       


    }
}

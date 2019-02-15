using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTrackerAPI.Models
{
    public class LifetimeTest
    {
        public DateTime Timestamp { get; }

        public LifetimeTest()
        {
            this.Timestamp = DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TaskTrackerAPI.AppExceptions
{
    public class TaskNotFoundException : Exception
    {
        public TaskNotFoundException()
        {
        }

        public TaskNotFoundException(string message) : base(message)
        {
        }

        public TaskNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TaskNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

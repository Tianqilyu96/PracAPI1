using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Models
{
    public class ApiError
    {
        public ApiError()
        {
                
        }

        public ApiError(string message)
        {
            Message = message;
        }

        public string Message { set; get; }

        public string Detail { set; get; }
    }
}

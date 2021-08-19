using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PracAPI1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Filters
{
    public class JsonExeptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;

        public JsonExeptionFilter(IHostingEnvironment env) //dependence injection
        {
            _env = env;
        }
        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();

            if (_env.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace;
            }
            else
            {
                error.Message = "A server error occured";
                error.Detail = context.Exception.Message;
            }

           
            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Controllers
{
    [Route("/")]
    [ApiController]
    public class RootController : ControllerBase   //controllerbase is for API, return links to all other routes and controllers
    {
        [HttpGet(Name = nameof(GetRoot))] //tell controller handle http get method
        public IActionResult GetRoot()  //return http status code or JSON 
        {
            var response = new { href = Url.Link(nameof(GetRoot),null) }; //the url link of route name/route para
            return Ok(response); //return 200 ok with response
        }
    }
}

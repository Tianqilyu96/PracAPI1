using Microsoft.AspNetCore.Mvc;
using PracAPI1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Controllers
{
    [Route("/")]
    [ApiController]
    [ApiVersion("1.0")] // set api version
    public class RootController : ControllerBase   //controllerbase is for API, return links to all other routes and controllers

    {
        //Route names can be used to generate a URL based on a specific route. Route names:
        //Have no impact on the URL matching behavior of routing.
        //Are only used for URL generation.
        //Route names must be unique application-wide.
        [HttpGet(Name = nameof(GetRoot))] //tell controller handle http get method
        [ProducesResponseType(200)] //optional
        public IActionResult GetRoot()  //return http status code or JSON 
        {
            //var response = new 
            //{ 
            //    href = Url.Link(nameof(GetRoot),null), 
            //    rooms = new
            //    {
            //        href = Url.Link(nameof(RoomsController.GetRooms),null)
            //    },

            //    info = new
            //    {
            //        href = Url.Link(nameof(InfoController.GetInfo),null)  // add info's href
            //    }
            //}; //the url link of route name/route para
            var response = new RootResponse
            {
                Self = Link.To(nameof(RootController.GetRoot)),
                Rooms = Link.ToCollection(nameof(RoomsController.GetAllRooms)),
                Info = Link.To(nameof(InfoController.GetInfo))

            };
            return Ok(response); //return 200 ok with response
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Controllers
{
    [ApiController]
    [Route("/[controller]")] // [controller] will match the name of the controller which is /Rooms
    public class RoomsController : ControllerBase //let client search room in the hotel
    {
        [HttpGet(Name = nameof(GetRooms))]
        public IActionResult GetRooms()
        {
            throw new NotImplementedException();
        } 
    }
}

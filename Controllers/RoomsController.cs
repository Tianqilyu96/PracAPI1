using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracAPI1.Models;
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
        private readonly HotelDbContext _context;

        public RoomsController(HotelDbContext context)
        {
            _context = context;
        }
        [HttpGet(Name = nameof(GetRooms))]
        public IActionResult GetRooms()
        {
            throw new NotImplementedException();
        } 

        [HttpGet("{id}",Name =nameof(GetRoomById))] //GET /Rooms/{id}
        [ProducesResponseType(404)]
        public async Task<ActionResult<Room>> GetRoomById(Guid id)
        {
            var entity = await _context.Room.SingleOrDefaultAsync(x => x.Id == id); //find the room by id in the database

            if(entity == null)
            {
                return NotFound();
            }

            var resource = new Room
            {
                Href = Url.Link(nameof(GetRoomById), new { id = entity.Id }),
                Name = entity.Name,
                Rate = (entity.Rate / 100.0m).ToString(),
            };
            return resource;
        }
    }
}

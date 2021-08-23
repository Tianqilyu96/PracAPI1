using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracAPI1.Models;
using PracAPI1.Services;
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
        private readonly IRoomService roomService;

        public RoomsController(IRoomService service)
        {
            roomService = service;
        }
        [HttpGet(Name = nameof(GetAllRooms))]
        public async Task<ActionResult<Collection<Room>>> GetAllRooms()
        {
            var rooms = await roomService.GetRoomsAsync();

            var collection = new Collection<Room>
            {
                Self = Link.To(nameof(GetAllRooms)),
                Value = rooms.ToArray()
            };

            return collection;
        } 

        [HttpGet("{id}",Name =nameof(GetRoomById))] //GET /Rooms/{id}
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Room>> GetRoomById(Guid id)
        {
            var room = await roomService.GetRoomAsync(id);
            if(room == null)
            {
                return NotFound();
            }
            return room;
        }
    }
}

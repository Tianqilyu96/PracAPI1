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
        private readonly IOpeningService _openingService;
        private readonly IDateLogicService dateLogicService;
        private readonly IBookingService bookingService;

        public RoomsController(IRoomService service, IOpeningService openingService, IDateLogicService dateLogicService,IBookingService bookingService)
        {
            roomService = service;
            _openingService = openingService;
            this.dateLogicService = dateLogicService;
            this.bookingService = bookingService;
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

        // GET /rooms/openings
        [HttpGet("openings", Name = nameof(GetAllRoomOpenings))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Collection<Opening>>> GetAllRoomOpenings([FromQuery]PagingOptions pagingOptions = null)
        {
            var openings = await _openingService.GetOpeningsAsync(pagingOptions);

            var collection = new PageCollection<Opening>()
            {
                Self = Link.ToCollection(nameof(GetAllRoomOpenings)),
                Value = openings.Items.ToArray(),
                Size = openings.TotalSize,
                OffSet = pagingOptions.OffSet.Value,
                Limit = pagingOptions.Limit.Value
            };

            return collection;
        }

        //POST /rooms/id/bookings
        [HttpPost("{id}/bookings",Name = nameof(CreateBookingForRoom))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CreateBookingForRoom(Guid id, [FromBody] BookingForm bookingForm)
        {
            var room = await roomService.GetRoomAsync(id);
            if (room == null) return NotFound();

            var minimumStay = dateLogicService.GetMinimumStay();
            bool tooShort = (bookingForm.EndAt.Value - bookingForm.StartAt.Value) < minimumStay;

            if (tooShort) return BadRequest(new ApiError($"The minimum stay is {minimumStay} hours"));

            var conflictSlots = await _openingService.GetConflictingSlots(id, bookingForm.StartAt.Value, bookingForm.EndAt.Value);
            if (conflictSlots.Any()) return BadRequest(new ApiError("This room is already booked by someone"));

            //TODO: Get user id
            var userId = Guid.NewGuid();

            var bookingId = await bookingService.CreateBookingAsync(userId, id, bookingForm.StartAt.Value, bookingForm.EndAt.Value);

            return Created(Url.Link(nameof(GetRoomById), new { bookingId }), null);

        }


    }
}

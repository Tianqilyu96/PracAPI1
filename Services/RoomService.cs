using Microsoft.EntityFrameworkCore;
using PracAPI1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Services
{
    public class RoomService : IRoomService
    {
        private readonly HotelDbContext _context;

        public RoomService(HotelDbContext context)
        {
            _context = context;
        }
        public async Task<Room> GetRoomAsync(Guid id)
        {
            var entity = await _context.Room.SingleOrDefaultAsync(x => x.Id == id); //find the room by id in the database

            if (entity == null)
            {
                return null;
            }

            return new Room
            {
                //Href = Url.Link(nameof(GetRoomById), new { id = entity.Id }), //can't use URL in service
                Name = entity.Name,
                Rate = (entity.Rate / 100.0m).ToString(),
            };
            
        }
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IConfigurationProvider configurationProvider;

        public RoomService(HotelDbContext context, IConfigurationProvider configurationProvider)
        {
            _context = context;
            
            this.configurationProvider = configurationProvider;
        }
        public async Task<Room> GetRoomAsync(Guid id)
        {
            var entity = await _context.Room.SingleOrDefaultAsync(x => x.Id == id); //find the room by id in the database

            if (entity == null)
            {
                return null;
            }

            //return new Room
            //{
            //    //Href = Url.Link(nameof(GetRoomById), new { id = entity.Id }), //can't use URL in service
            //    Name = entity.Name,
            //    Rate = (entity.Rate / 100.0m).ToString(),

            //};
            var mapper = configurationProvider.CreateMapper();
            return mapper.Map<Room>(entity);//map from entity object to room
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            var query = _context.Room.ProjectTo<Room>(configurationProvider);

            return await query.ToListAsync();
        }
    }
}

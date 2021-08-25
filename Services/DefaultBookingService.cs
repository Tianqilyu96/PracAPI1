using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracAPI1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Services
{
    public class DefaultBookingService : IBookingService
    {
        private readonly HotelDbContext _context;
        private readonly IDateLogicService _dateLogicService;
        private readonly IMapper _mapper;

        public DefaultBookingService(
            HotelDbContext context,
            IDateLogicService dateLogicService,
            IMapper mapper)
        {
            _context = context;
            _dateLogicService = dateLogicService;
            _mapper = mapper;
        }

        public async Task<Guid> CreateBookingAsync(
            Guid userId,
            Guid roomId,
            DateTimeOffset startAt,
            DateTimeOffset endAt)
        {
            // TODO: Save the new booking to the database
            var room = await _context.Room.SingleOrDefaultAsync(r => r.Id == roomId);
            if (room == null) throw new ArgumentException("Invalid room ID");

            var minimumstay = _dateLogicService.GetMinimumStay();
            var total = (int)((endAt - startAt).TotalHours/minimumstay.TotalHours)*room.Rate;

            var id = Guid.NewGuid();

            var newBooking = _context.Bookings.Add(new BookingEntity
            {
                Id = id,
                CreatedAt = DateTimeOffset.UtcNow,
                ModifiedAt = DateTimeOffset.UtcNow,
                StartAt = startAt.ToUniversalTime(),
                EndAt = endAt.ToUniversalTime(),
                Room = room,
                Total = total
            });

            var created = await _context.SaveChangesAsync(); //it return a int, which is how many entities are affected
            if(created < 1) throw new InvalidOperationException("Could not create booking");
            return roomId;
        }

        public async Task<Bookings> GetBookingAsync(Guid bookingId)
        {
            var entity = await _context.Bookings
                .SingleOrDefaultAsync(b => b.Id == bookingId);

            if (entity == null) return null;

            return _mapper.Map<Bookings>(entity);
        }
    }
}

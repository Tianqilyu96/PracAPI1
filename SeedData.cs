using Microsoft.Extensions.DependencyInjection;
using PracAPI1.Models;
using PracAPI1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider service)
        {
            await AddTestData(service.GetRequiredService<HotelDbContext>(), service.GetRequiredService<IDateLogicService>());
            
        }
        public static async Task AddTestData(HotelDbContext context, IDateLogicService dateLogicService)
        {
            if (context.Room.Any())
            {
                //if already has data, do nothing
                return;
            }
            var oxford = context.Room.Add(new RoomEntity
            {
                Id = Guid.Parse("301df04d-8679-4b1b-ab92-0a586ae53d08"),
                Name = "No.1 Suit",
                Rate = 11111,
            }).Entity;

            context.Room.Add(new RoomEntity
            {
                Id = Guid.Parse("ee2b83be-91db-4de5-8122-35a9e9195976"),
                Name = "No.2 Suit",
                Rate = 22222,
            });

            var today = DateTimeOffset.Now;
            var start = dateLogicService.AlignStartTime(today);
            var end = start.Add(dateLogicService.GetMinimumStay());

            context.Bookings.Add(new BookingEntity
            {
                Id = Guid.Parse("2eac8dea-2749-42b3-9d21-8eb2fc0fd6bd"),
                Room = oxford,
                CreatedAt = DateTimeOffset.UtcNow,
                StartAt = start,
                EndAt = end,
                Total = oxford.Rate,
            });

            await context.SaveChangesAsync();
        }
    }
}

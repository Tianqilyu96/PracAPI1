using Microsoft.Extensions.DependencyInjection;
using PracAPI1.Models;
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
            await AddTestData(service.GetRequiredService<HotelDbContext>());
        }
        public static async Task AddTestData(HotelDbContext context)
        {
            if (context.Room.Any())
            {
                //if already has data, do nothing
                return;
            }
            context.Room.Add(new RoomEntity
            {
                Id = Guid.Parse("301df04d-8679-4b1b-ab92-0a586ae53d08"),
                Name = "No.1 Suit",
                Rate = 11111,
            });

            context.Room.Add(new RoomEntity
            {
                Id = Guid.Parse("ee2b83be-91db-4de5-8122-35a9e9195976"),
                Name = "No.2 Suit",
                Rate = 22222,
            });

            await context.SaveChangesAsync();
        }
    }
}

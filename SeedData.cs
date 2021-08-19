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
                Id = Guid.Parse("0001"),
                Name = "No.1 Suit",
                Rate = 11111,
            });

            context.Room.Add(new RoomEntity
            {
                Id = Guid.Parse("0002"),
                Name = "No.2 Suit",
                Rate = 22222,
            });

            await context.SaveChangesAsync();
        }
    }
}

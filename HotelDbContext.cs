using Microsoft.EntityFrameworkCore;
using PracAPI1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions options) :base(options)
        {

        }
        public DbSet<RoomEntity> Room { get; set; }

        public DbSet<BookingEntity> Bookings { get; set; }
    }
}

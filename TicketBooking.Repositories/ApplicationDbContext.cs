
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Entities;

namespace TicketBooking.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>// ORM Object Relational Mapper
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Bus> BusInfo { get; set; }
        public DbSet<BusSeatDetail> SeatDetails { get; set; }
        public DbSet<Booking> Booking { get; set; }



       


    }
}

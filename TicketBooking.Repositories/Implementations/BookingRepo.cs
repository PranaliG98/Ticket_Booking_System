using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Entities;
using TicketBooking.Repositories.Interfaces;

namespace TicketBooking.Repositories.Implementations
{
    public class BookingRepo : IBookingRepo
    {
        private readonly ApplicationDbContext _context;

        public BookingRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllBooking(string AppUserId = null)
        {
            IEnumerable<Booking> bookings;
            if (AppUserId== null)
            {
                bookings = await _context.Booking.Include(z => z.ApplicationUser).Include(x => x.BusSeatDetail).ThenInclude(y => y.Bus).ToListAsync();
                return bookings;
            }
            bookings = await _context.Booking.Include(z => z.ApplicationUser).Include(x => x.BusSeatDetail).ThenInclude(y => y.Bus).Where(a=>a.ApplicationUserId==AppUserId).ToListAsync();
            return bookings;

        }

        public async Task<IEnumerable<Booking>> GetTodaysBooking(int busId, DateTime journeyDate)
        {
            var today = journeyDate.Date;
            var booking  = await  _context.Booking.Include(y=>y.BusSeatDetail)
                .ThenInclude(z=>z.Bus).Where(x=>x.Date.Date==today && x.BusSeatDetail.BusId==busId).ToListAsync();
            return booking;
        }

        public async Task SaveBooking(List<Booking> booking)
        {
           await _context.Booking.AddRangeAsync(booking);
            await _context.SaveChangesAsync();
        }
    }
}

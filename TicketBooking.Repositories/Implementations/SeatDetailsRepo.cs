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
    public class SeatDetailsRepo : ISeatDetailsRepo
    {
        private readonly ApplicationDbContext _context;

        public SeatDetailsRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckExist(int seatNumber, int busId)
        {
           return await _context.SeatDetails.AnyAsync(x=>x.SeatNumber==seatNumber && x.BusId==busId);
        }

        public async Task Delete(BusSeatDetail seatDetails)
        {
           _context.SeatDetails.Remove(seatDetails);    
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BusSeatDetail>> GetALL()
        {
            return await _context.SeatDetails.Include(x=>x.Bus).ToListAsync();
        }

        public async Task<BusSeatDetail> GetById(int id)
        {
            return await _context.SeatDetails.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Insert(BusSeatDetail seatDetails)
        {
            await _context.SeatDetails.AddAsync(seatDetails);
            await _context.SaveChangesAsync();
        }

        public Task Update(BusSeatDetail seatDetails)
        {
            throw new NotImplementedException();
        }
    }
}

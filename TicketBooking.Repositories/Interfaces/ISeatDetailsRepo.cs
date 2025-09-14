using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Entities;

namespace TicketBooking.Repositories.Interfaces
{
    public interface ISeatDetailsRepo
    {
        Task<IEnumerable<BusSeatDetail>> GetALL();
        Task<BusSeatDetail> GetById(int id);
        Task Insert(BusSeatDetail seatDetails);
        Task Update(BusSeatDetail seatDetails);
        Task Delete (BusSeatDetail seatDetails);
        Task<bool> CheckExist(int seatNumber, int busId);
    }
}

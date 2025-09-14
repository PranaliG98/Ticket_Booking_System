using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Entities;

namespace TicketBooking.Repositories.Interfaces
{
    public interface IBusRepo
    {
        Task<IEnumerable<Bus>> GetALL();
        Task<Bus> GetById(int id);
        Task Insert(Bus bus);
        Task Update(Bus bus);
        Task Delete (Bus bus);


    }
}

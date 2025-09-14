using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int BusSeatDetailId { get; set; }
        public BusSeatDetail BusSeatDetail { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

    }
}

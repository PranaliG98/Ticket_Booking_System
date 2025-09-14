using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Web.ViewModels.BookingVM
{
    public class TicketBookingViewModel
    {
        public int BookingId { get; set; }
        public string  UserId { get; set; }
        public int SeatNumber { get; set; }
        public string BusNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime JourneyDate { get; set; }

    }
}

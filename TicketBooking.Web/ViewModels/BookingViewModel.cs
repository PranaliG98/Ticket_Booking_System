using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Web.ViewModels
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public string BusImage { get; set; }
        public string BusNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime JourneyDate { get; set; }
        public List<CheckBoxTable> SeatDetail { get; set; } = new List<CheckBoxTable>();


    }
    public class CheckBoxTable
    {
        public int Id { get; set; }
        public int SeatNumber { get; set; }
        public string SeatImage { get; set; }
        public bool IsChecked { get; set; }
    }
}

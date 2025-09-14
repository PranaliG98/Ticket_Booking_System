using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Entities;

namespace TicketBooking.Web.ViewModels.SeatVM
{
    public class EditSeatDetailViewModel
    {
        public int Id { get; set; }
        [Range(1, 50)]
        public int SeatNumber { get; set; }
        public int BusId { get; set; }

        public SeatStatus SeatStatus { get; set; }
    }
}

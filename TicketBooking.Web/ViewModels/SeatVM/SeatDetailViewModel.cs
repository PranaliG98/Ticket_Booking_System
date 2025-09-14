using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Entities;

namespace TicketBooking.Web.ViewModels.SeatVM
{
    public class SeatDetailViewModel
    {
        public int Id { get; set; }
       
        public int SeatNumber { get; set; }
        public string BusNumber { get; set; }     
       
        public string  SeatStatus { get; set; }

    }
}

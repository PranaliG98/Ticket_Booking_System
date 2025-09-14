using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TicketBooking.Entities
{
    public class BusSeatDetail
    {
        public int Id { get; set; }
        [Range(1, 50)]
        public int SeatNumber { get; set; }       
        public int BusId { get; set; }
        
        public Bus Bus { get; set; }
        public SeatStatus SeatStatus { get; set; }
        [JsonIgnore]
        public ICollection<Booking> Bookings { get; set; }

    }
    public enum SeatStatus
    {
       
        Available,
        Damaged,
        Booked
    }
}

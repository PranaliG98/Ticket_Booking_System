using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Entities
{
    public class Bus
    {
        public int Id { get; set; }
       
        public required string BusNumber { get; set; }
        [Range(1,50)]
        public required int MaxSeatCapacity { get; set; }
        public string? BusImage { get; set; }
      
        public ICollection<BusSeatDetail> BusSeatDetails { get; set; }
            = new List<BusSeatDetail>();

    }
}

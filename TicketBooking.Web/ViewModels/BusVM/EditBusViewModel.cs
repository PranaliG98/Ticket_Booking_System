using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Web.ViewModels.BusVM
{
    public class EditBusViewModel
    {
        public int Id { get; set; }
        public required string BusNumber { get; set; }
        [Range(1, 50)]
        public required int MaxSeatCapacity { get; set; }
        public IFormFile BusImageFile { get; set; }
        public string BusImage { get; set; }

    }
}

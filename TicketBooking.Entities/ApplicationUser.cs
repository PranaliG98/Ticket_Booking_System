﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Entities
{
    public class ApplicationUser :  IdentityUser
    {
        public string? FullName { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}

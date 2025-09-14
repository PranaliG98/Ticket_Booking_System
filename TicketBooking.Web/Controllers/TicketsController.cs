using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketBooking.Entities;
using TicketBooking.Infrastructure;
using TicketBooking.Repositories.Interfaces;
using TicketBooking.Web.ViewModels;
using TicketBooking.Web.ViewModels.BookingVM;

namespace TicketBooking.Web.Controllers
{
    public class TicketsController : Controller
    {
        private IBookingRepo _bookingRepo;

        public TicketsController(IBookingRepo bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

       
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Booking> bookings=null;
            List<TicketBookingViewModel> vm =  new List<TicketBookingViewModel>();
            if(User.IsInRole(GlobalConfiguration.Admin_Role))
            {
                bookings = await _bookingRepo.GetAllBooking();
            }
            if(User.IsInRole(GlobalConfiguration.Customer_Role))
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                var userId = claim.Value;
                bookings = await _bookingRepo.GetAllBooking(userId);

            }
            foreach (var booking in bookings)
            {
                vm.Add(new TicketBookingViewModel
                {
                    BookingId = booking.Id,
                    SeatNumber = booking.BusSeatDetail.SeatNumber,
                    BusNumber = booking.BusSeatDetail.Bus.BusNumber,
                    UserId = booking.ApplicationUserId,
                    JourneyDate = booking.Date

                });

            }
            return View(vm);

        }
    }
}

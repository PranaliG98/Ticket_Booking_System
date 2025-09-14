using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using TicketBooking.Entities;
using TicketBooking.Infrastructure;
using TicketBooking.Repositories.Interfaces;
using TicketBooking.Web.Models;
using TicketBooking.Web.ViewModels;
using TicketBooking.Web.ViewModels.BusVM;

namespace TicketBooking.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IBusRepo _busRepo;
        private IMapper _mapper;
        private IBookingRepo _bookingRepo;

        public HomeController(ILogger<HomeController> logger, IBusRepo busRepo,
            IMapper mapper, IBookingRepo bookingRepo)
        {
            _logger = logger;
            _busRepo = busRepo;
            _mapper = mapper;
            _bookingRepo = bookingRepo;
        }

        public async Task<IActionResult> Index()
        {
            var buses = await _busRepo.GetALL();
            var busViewModel = _mapper.Map<List<BusViewModel>>(buses);
             return View(busViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> GetSeatDetailsByDate(int busId, DateTime journeyDate)
        {
            var booking = _bookingRepo.GetTodaysBooking(busId, journeyDate).GetAwaiter().GetResult()
     .Select(x => x.BusSeatDetailId).ToList();
            var busInfo = await _busRepo.GetById(busId); 
            List<CheckBoxTable> seatDetails =  new List<CheckBoxTable>();
            foreach (var BusSeat in busInfo.BusSeatDetails)
            {
                seatDetails.Add(new CheckBoxTable
                {
                    Id = BusSeat.Id,
                    SeatImage = booking.Contains(BusSeat.Id) ? "RedChair.png" : "GreenChair.png",
                    IsChecked = booking.Contains(BusSeat.Id),
                    SeatNumber = BusSeat.SeatNumber

                });

            }

            return Json(seatDetails);
        }

        [HttpGet]
        public async Task<IActionResult> TicketBook(int id)
        {
            BookingViewModel vm = new BookingViewModel();
            var busInfo = await _busRepo.GetById(id);
            var booking =  _bookingRepo.GetTodaysBooking(busInfo.Id,DateTime.Today).GetAwaiter().GetResult()
                .Select(x=>x.BusSeatDetailId).ToList();
            vm.BusImage = busInfo.BusImage;
            vm.BusNumber = busInfo.BusNumber;
            vm.Id = busInfo.Id;
            vm.JourneyDate = DateTime.Today;
            foreach (var BusSeat in busInfo.BusSeatDetails)
            {
                vm.SeatDetail.Add(new CheckBoxTable
                {
                    Id = BusSeat.Id,
                    SeatImage = booking.Contains(BusSeat.Id) ? "RedChair.png" : "GreenChair.png",
                    IsChecked = booking.Contains(BusSeat.Id),
                    SeatNumber =  BusSeat.SeatNumber

                });
            }
            return View(vm);

        }
        [HttpPost]
        [Authorize(Roles =GlobalConfiguration.Customer_Role)]
        public async Task<IActionResult> TicketBook(BookingViewModel vm)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claims.Value;


            List<Booking> booking = new List<Booking>();
            var seatdetails =  vm.SeatDetail.Where(x=>x.IsChecked==true).Select(x=>x.Id).ToList();
            var journeyDate = vm.JourneyDate;
            foreach (var seatDetailId in seatdetails)
            {
                booking.Add(new Booking
                {
                    Date = journeyDate,
                    BusSeatDetailId = seatDetailId,
                    ApplicationUserId = userId
                });

            }
           await _bookingRepo.SaveBooking(booking);
            TempData["success"] = "Your Tickets book Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using TicketBooking.Entities;
using TicketBooking.Infrastructure;
using TicketBooking.Repositories.Interfaces;
using TicketBooking.Web.ViewModels.SeatVM;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TicketBooking.Web.Controllers
{
    [Authorize(Roles = GlobalConfiguration.Admin_Role)]
    public class SeatDetailsController : Controller
    {
        private ISeatDetailsRepo _seatDetailRepo;
        private IBusRepo _busRepo;
        private IMapper _mapper;

        public SeatDetailsController(ISeatDetailsRepo seatDetailRepo,
            IBusRepo busRepo,
            IMapper mapper)
        {
            _seatDetailRepo = seatDetailRepo;
            _busRepo = busRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            //var seatDetails = await _seatDetailRepo.GetALL();
            //var vm = _mapper.Map<List<SeatDetailViewModel>>(seatDetails);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetSeatDetails()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var seatDetails = _seatDetailRepo.GetALL().GetAwaiter().GetResult();

                if (!string.IsNullOrEmpty(searchValue))
                {
                    seatDetails = seatDetails.Where(m => m.Bus.BusNumber.Contains(searchValue)
                                                );
                }
                recordsTotal = seatDetails.Count();
                var data = seatDetails.Skip(skip).Take(pageSize).ToList();
                var vm = _mapper.Map<List<SeatDetailViewModel>>(data);

                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = vm };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var buses = await _busRepo.GetALL();
            ViewBag.busesList = new SelectList(buses, "Id", "BusNumber");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSeatViewModel vm)
        {
            var model = _mapper.Map<BusSeatDetail>(vm);
            if(!await _seatDetailRepo.CheckExist(vm.SeatNumber,vm.BusId))
            {
                await _seatDetailRepo.Insert(model);
            }         
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var seatDetail = await _seatDetailRepo.GetById(id);
            var buses = await _busRepo.GetALL();
            ViewBag.busesList = new SelectList(buses, "Id", "BusNumber");
            var vm = _mapper.Map<EditSeatDetailViewModel>(seatDetail);
            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditSeatDetailViewModel vm)
        {
            var model = await _seatDetailRepo.GetById(vm.Id);
            if (!await _seatDetailRepo.CheckExist(vm.SeatNumber, vm.BusId))
            {
                model = _mapper.Map(vm, model);
                await _seatDetailRepo.Update(model);
                
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var seatDetail =  await _seatDetailRepo.GetById(id);
            await _seatDetailRepo.Delete(seatDetail);
            return RedirectToAction("Index");
        }
    }
}

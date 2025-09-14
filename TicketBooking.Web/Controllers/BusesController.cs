using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using TicketBooking.Web.Repositories.Interfaces;
using TicketBooking.Entities;
using TicketBooking.Repositories.Interfaces;
using TicketBooking.Web.ViewModels.BusVM;
using Microsoft.AspNetCore.Authorization;
using TicketBooking.Infrastructure;

namespace TicketBooking.Web.Controllers
{
    [Authorize(Roles =GlobalConfiguration.Admin_Role)]
    public class BusesController : Controller
    {
        private readonly IBusRepo _busRepo;
        private IMapper _mapper;
        private IUtilityRepo _utilityRepo;
        private string BusImage = "BusImage";

        public BusesController(IBusRepo busRepo, IMapper mapper, IUtilityRepo utilityRepo)
        {
            _busRepo = busRepo;
            _mapper = mapper;
            _utilityRepo = utilityRepo;
        }

        public async Task<IActionResult> Index()
        {
            var buses = await _busRepo.GetALL();
            var vm  = _mapper.Map<List<BusViewModel>>(buses);
            return View(vm);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBusViewModel vm)
        {
            var model =  _mapper.Map<Bus>(vm);
            if(vm.BusImage!=null)
            {
                model.BusImage = await _utilityRepo.SaveImagePath(BusImage, vm.BusImage);
            }
            await _busRepo.Insert(model);
            TempData["success"] = "Your Record Added";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var busDetail = await _busRepo.GetById(id);
            var vm  =  _mapper.Map<EditBusViewModel>(busDetail);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditBusViewModel vm)
        {
            var bus =await _busRepo.GetById(vm.Id);
              
            if (vm.BusImageFile != null)
            {
                bus.BusImage = await _utilityRepo.EditFilePath(BusImage, vm.BusImageFile, bus.BusImage);
            }
            bus = _mapper.Map(vm, bus);
            await _busRepo.Update(bus);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var bus = await _busRepo.GetById(id);
           await  _busRepo.Delete(bus);
            return RedirectToAction("Index");
        }


    }
}

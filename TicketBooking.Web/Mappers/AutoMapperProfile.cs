using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Entities;
using TicketBooking.Web.ViewModels;
using TicketBooking.Web.ViewModels.BusVM;
using TicketBooking.Web.ViewModels.SeatVM;

namespace TicketBooking.Web.Mappers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Bus, BusViewModel>();
            CreateMap<CreateBusViewModel, Bus>().
                ForMember(dest => dest.BusImage, opt => opt.Ignore());
            CreateMap<Bus, EditBusViewModel>().
    ForMember(dest => dest.BusImageFile, opt => opt.Ignore());
            CreateMap<EditBusViewModel, Bus>().
                 ForMember(dest => dest.BusImage, opt => opt.Ignore());
            CreateMap<BusSeatDetail,SeatDetailViewModel>()
                .ForMember(dest=>dest.BusNumber, opt => opt.MapFrom(src=>src.Bus.BusNumber))
                .ForMember(dest=>dest.SeatStatus,opt=>opt.MapFrom(src=>src.SeatStatus.ToString()));
            CreateMap<CreateSeatViewModel, BusSeatDetail>();
            CreateMap<BusSeatDetail,EditSeatDetailViewModel>().ReverseMap();   
           

        }

       
    }
}

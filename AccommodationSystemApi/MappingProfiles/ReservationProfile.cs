using AutoMapper;
using BLL.Models;
using DAL.Entities;

namespace AccommodationSystemApi.MappingProfiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<PostReservationRequestModel, Reservation>();
        }
    }
}

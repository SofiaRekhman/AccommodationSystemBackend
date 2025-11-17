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

            CreateMap<Reservation, GetReservationsResponseModel>()
                .ForMember(dest => dest.ReservationStartDate, i => i.MapFrom(src => src.StartDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.CreatedAt, i => i.MapFrom(src => src.CreatedAt.ToString("yyyy-MM-dd")));
        }
    }
}

using AutoMapper;
using BLL.Models;
using DAL.Entities;

namespace AccommodationSystemApi.MappingProfiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<PostReservationRequestModel, Reservation>()
                .ForMember(dest => dest.StartDate, i => i.MapFrom(src => src.ReservationStartDate))
                .ForMember(dest => dest.EndDate, i => i.MapFrom(src => src.ReservationEndDate))
                .ForMember(dest => dest.Reason, i => i.MapFrom(src => src.ReservationReason));

            CreateMap<Reservation, GetReservationsResponseModel>()
                .ForMember(dest => dest.ReservationStartDate, i => i.MapFrom(src => src.StartDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.ReservationStatusName, i => i.MapFrom(src => src.Status.StatusName))
                .ForMember(dest => dest.CreatedAt, i => i.MapFrom(src => src.CreatedAt.ToString("yyyy-MM-dd")));
        }
    }
}

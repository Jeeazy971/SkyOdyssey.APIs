using AutoMapper;
using SkyOdyssey.Models;
using SkyOdyssey.DTOs;

namespace SkyOdyssey.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<CreateReservationDto, Reservation>();
            CreateMap<UpdateReservationDto, Reservation>();
        }
    }
}

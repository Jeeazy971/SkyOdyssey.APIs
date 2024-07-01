using AutoMapper;
using SkyOdyssey.DTOs;
using SkyOdyssey.Models;

namespace SkyOdyssey.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map User to UserDto and vice versa
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            // Map RegisterUserDto to User
            CreateMap<RegisterUserDto, User>();

            // Other mappings
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<CreateLocationRequest, LocationDto>().ReverseMap();
            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<Flight, FlightDto>().ReverseMap();
            CreateMap<Hotel, HotelDto>().ReverseMap();
            CreateMap<CreateReservationDto, Reservation>().ReverseMap();
            CreateMap<UpdateReservationDto, Reservation>().ReverseMap();
        }
    }
}

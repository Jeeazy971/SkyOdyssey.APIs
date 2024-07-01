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
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<RegisterUserDto, User>();

            // Map Location to LocationDto and vice versa
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<CreateLocationRequest, LocationDto>().ReverseMap();

            // Map Reservation to ReservationDto and vice versa
            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ReverseMap();
            CreateMap<CreateReservationDto, Reservation>().ReverseMap();
            CreateMap<UpdateReservationDto, Reservation>().ReverseMap();

            // Map Flight to FlightDto and vice versa
            CreateMap<Flight, FlightDto>().ReverseMap();

            // Map Hotel to HotelDto and vice versa
            CreateMap<Hotel, HotelDto>().ReverseMap();
        }
    }
}

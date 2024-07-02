using AutoMapper;
using SkyOdyssey.DTOs;
using SkyOdyssey.Models;

namespace SkyOdyssey.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Faire correspondre User à UserDto et vice-versa
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            // Associer RegisterUserDto à l'utilisateur
            CreateMap<RegisterUserDto, User>();

            // Autres mappings
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<CreateLocationRequest, LocationDto>().ReverseMap();
            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.Locations, opt => opt.MapFrom(src => src.Locations))
                .ForMember(dest => dest.Flights, opt => opt.MapFrom(src => src.Flights))
                .ReverseMap();
            CreateMap<Flight, FlightDto>().ReverseMap();
            CreateMap<Hotel, HotelDto>().ReverseMap();
            CreateMap<CreateReservationDto, Reservation>().ReverseMap();
            CreateMap<UpdateReservationDto, Reservation>().ReverseMap();
        }
    }
}

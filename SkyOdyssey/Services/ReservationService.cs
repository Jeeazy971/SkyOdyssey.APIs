using AutoMapper;
using SkyOdyssey.DTOs;
using SkyOdyssey.Models;
using SkyOdyssey.Repositories;
using SkyOdyssey.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ReservationService(IReservationRepository reservationRepository, ILocationRepository locationRepository, IUserRepository userRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _locationRepository = locationRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
    {
        var reservations = await _reservationRepository.GetAllAsync();
        var reservationDtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        return reservationDtos;
    }

    public async Task<ReservationDto> GetReservationByIdAsync(int id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);
        var reservationDto = _mapper.Map<ReservationDto>(reservation);
        return reservationDto;
    }

    public async Task CreateReservationAsync(CreateReservationDto createReservationDto)
    {
        var reservation = _mapper.Map<Reservation>(createReservationDto);
        await _reservationRepository.AddAsync(reservation);
    }

    public async Task UpdateReservationAsync(int id, UpdateReservationDto updateReservationDto)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);
        if (reservation != null)
        {
            _mapper.Map(updateReservationDto, reservation);
            await _reservationRepository.UpdateAsync(reservation);
        }
    }

    public async Task DeleteReservationAsync(int id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);
        if (reservation != null)
        {
            await _reservationRepository.DeleteAsync(reservation);
        }
    }
}

using AutoMapper;
using SkyOdyssey.DTOs;
using SkyOdyssey.Models;
using SkyOdyssey.Repositories;
using SkyOdyssey.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
    {
        var reservations = await _reservationRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
    }

    public async Task<ReservationDto> GetReservationByIdAsync(int id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);
        return _mapper.Map<ReservationDto>(reservation);
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

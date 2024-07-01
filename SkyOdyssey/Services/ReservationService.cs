using AutoMapper;
using SkyOdyssey.DTOs;
using SkyOdyssey.Models;
using SkyOdyssey.Repositories;
using SkyOdyssey.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IFlightRepository _flightRepository;
    private readonly IMapper _mapper;

    public ReservationService(IReservationRepository reservationRepository, ILocationRepository locationRepository, IUserRepository userRepository, IFlightRepository flightRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _locationRepository = locationRepository;
        _userRepository = userRepository;
        _flightRepository = flightRepository;
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
        var user = await _userRepository.GetByIdAsync(createReservationDto.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var locations = await _locationRepository.GetByIdsAsync(createReservationDto.LocationIds);
        if (locations.Count() != createReservationDto.LocationIds.Count)
        {
            throw new Exception("One or more locations not found");
        }

        var flights = await _flightRepository.GetByIdsAsync(createReservationDto.FlightIds);
        if (flights.Count() != createReservationDto.FlightIds.Count)
        {
            throw new Exception("One or more flights not found");
        }

        var reservation = _mapper.Map<Reservation>(createReservationDto);
        reservation.User = user;
        reservation.Locations = locations.ToList();
        reservation.Flights = flights.ToList();

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

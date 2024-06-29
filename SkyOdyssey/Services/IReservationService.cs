﻿using SkyOdyssey.DTOs;

namespace SkyOdyssey.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
        Task<ReservationDto> GetReservationByIdAsync(int id);
        Task CreateReservationAsync(CreateReservationDto createReservationDto);
        Task UpdateReservationAsync(int id, UpdateReservationDto updateReservationDto);
        Task DeleteReservationAsync(int id);
    }
}

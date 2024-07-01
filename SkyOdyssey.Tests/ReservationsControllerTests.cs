using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SkyOdyssey.Controllers;
using SkyOdyssey.DTOs;
using SkyOdyssey.Models;
using SkyOdyssey.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SkyOdyssey.Tests.Controllers
{
    public class ReservationsControllerTests
    {
        private readonly Mock<IReservationService> _reservationServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ReservationsController _reservationsController;

        public ReservationsControllerTests()
        {
            _reservationServiceMock = new Mock<IReservationService>();
            _mapperMock = new Mock<IMapper>();
            _reservationsController = new ReservationsController(_reservationServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllReservations_ShouldReturnOkResult_WithAllReservations()
        {
            // Arrange
            var reservations = new List<ReservationDto> { new ReservationDto { Id = 1 }, new ReservationDto { Id = 2 } };
            _reservationServiceMock.Setup(service => service.GetAllReservationsAsync()).ReturnsAsync(reservations);

            // Act
            var result = await _reservationsController.GetAllReservations();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(reservations, okResult.Value);
        }

        [Fact]
        public async Task GetReservationById_ShouldReturnOkResult_WhenReservationExists()
        {
            // Arrange
            var reservation = new ReservationDto { Id = 1 };
            _reservationServiceMock.Setup(service => service.GetReservationByIdAsync(1)).ReturnsAsync(reservation);

            // Act
            var result = await _reservationsController.GetReservationById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(reservation, okResult.Value);
        }

        [Fact]
        public async Task GetReservationById_ShouldReturnNotFoundResult_WhenReservationDoesNotExist()
        {
            // Arrange
            _reservationServiceMock.Setup(service => service.GetReservationByIdAsync(1)).ReturnsAsync((ReservationDto)null);

            // Act
            var result = await _reservationsController.GetReservationById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Additional tests for CreateReservation, UpdateReservation, DeleteReservation, etc.
    }
}

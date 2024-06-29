using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SkyOdyssey.Services;
using SkyOdyssey.DTOs;
using Stripe;
using System.Threading.Tasks;

namespace SkyOdyssey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IConfiguration _configuration;

        public PaymentsController(IReservationService reservationService, IConfiguration configuration)
        {
            _reservationService = reservationService;
            _configuration = configuration;
        }

        [HttpPost("{reservationId}/pay")]
        public async Task<IActionResult> Pay(int reservationId, [FromBody] PaymentDto paymentDto)
        {
            if (paymentDto == null)
            {
                return BadRequest("Payment request cannot be null");
            }

            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);
            if (reservation == null)
            {
                return NotFound($"Reservation with ID {reservationId} not found");
            }

            if (string.IsNullOrEmpty(paymentDto.Token))
            {
                return BadRequest("Payment token is required");
            }

            // Configurer Stripe
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            if (string.IsNullOrEmpty(StripeConfiguration.ApiKey))
            {
                return StatusCode(500, "Stripe API key is not configured");
            }

            var options = new ChargeCreateOptions
            {
                Amount = (long)(paymentDto.Amount * 100), // Montant en centimes
                Currency = paymentDto.Currency,
                Description = $"Payment for reservation {paymentDto.ReservationId}",
                Source = paymentDto.Token
            };

            var service = new ChargeService();
            Charge charge;
            try
            {
                charge = service.Create(options);
            }
            catch (StripeException ex)
            {
                return StatusCode(500, $"Stripe error: {ex.Message}");
            }

            if (charge.Status == "succeeded")
            {
                var updateReservationDto = new UpdateReservationDto
                {
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    NumberOfGuests = reservation.NumberOfGuests,
                    TotalPrice = reservation.TotalPrice,
                    UserId = reservation.UserId,
                    LocationId = reservation.LocationId,
                    Status = "Paid",
                    Flights = reservation.Flights,
                    Hotels = reservation.Hotels
                };

                await _reservationService.UpdateReservationAsync(reservation.Id, updateReservationDto);
                return Ok();
            }

            return BadRequest(new { error = "Payment failed" });
        }
    }
}

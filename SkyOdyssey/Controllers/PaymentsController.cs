using Microsoft.AspNetCore.Mvc;
using SkyOdyssey.Services;
using SkyOdyssey.DTOs;
using Stripe;

namespace SkyOdyssey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public PaymentsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("{reservationId}/pay")]
        public async Task<IActionResult> Pay(int reservationId, [FromBody] PaymentDto paymentDto)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);
            if (reservation == null)
                return NotFound();

            var options = new ChargeCreateOptions
            {
                Amount = (long)(reservation.TotalPrice * 100), // Stripe amount is in cents
                Currency = "usd",
                Description = $"Payment for reservation {reservationId}",
                Source = paymentDto.Token,
            };

            var service = new ChargeService();
            Charge charge;

            try
            {
                charge = service.Create(options);
            }
            catch (StripeException e)
            {
                return BadRequest(new { error = e.StripeError.Message });
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
            else
            {
                return BadRequest(new { error = "Payment failed" });
            }
        }
    }
}

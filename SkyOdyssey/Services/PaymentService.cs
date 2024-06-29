using SkyOdyssey.DTOs;
using Stripe;

namespace SkyOdyssey.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<Charge> ProcessPaymentAsync(PaymentDto paymentDto)
        {
            var options = new ChargeCreateOptions
            {
                Amount = (long)(paymentDto.Amount * 100),
                Currency = paymentDto.Currency,
                Description = $"Paiement pour la réservation {paymentDto.ReservationId}",
                Source = paymentDto.Token,
            };

            var service = new ChargeService();
            Charge charge = await service.CreateAsync(options);
            return charge;
        }
    }
}

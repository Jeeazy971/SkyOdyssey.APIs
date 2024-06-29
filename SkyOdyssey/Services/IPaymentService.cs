using SkyOdyssey.DTOs;
using Stripe;

namespace SkyOdyssey.Services
{
    public interface IPaymentService
    {
        Task<Charge> ProcessPaymentAsync(PaymentDto paymentDto);
    }
}

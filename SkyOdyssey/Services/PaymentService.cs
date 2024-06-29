using Microsoft.Extensions.Configuration;
using Stripe;
using System.Threading.Tasks;

namespace SkyOdyssey.Services
{
    public class PaymentService
    {
        public PaymentService(IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:ApiKey"];
        }

        public async Task<PaymentIntent> CreatePaymentIntent(decimal amount, string currency = "eur")
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // Convertir en cents
                Currency = currency,
                PaymentMethodTypes = new List<string> { "card" },
            };
            var service = new PaymentIntentService();
            return await service.CreateAsync(options);
        }
    }
}

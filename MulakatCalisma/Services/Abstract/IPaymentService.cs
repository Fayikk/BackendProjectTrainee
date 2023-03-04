
using Stripe.Checkout;

namespace MulakatCalisma.Services.Abstract
{
    public interface IPaymentService
    {
        Task<Session> CreateCheckoutSession();
    }
}

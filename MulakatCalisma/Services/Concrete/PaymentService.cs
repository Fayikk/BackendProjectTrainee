using MulakatCalisma.Services.Abstract;
using Stripe;
using Stripe.Checkout;

namespace MulakatCalisma.Services.Concrete
{
    public class PaymentService : IPaymentService
    {
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;
        public PaymentService(IOrderService orderService, IAuthService authService)
        {
            StripeConfiguration.ApiKey = "sk_test_51MIUdEB01rwBMrJ2DnweLtJJc3ijAgmJIlBOqsMy0txUJH3B6D08q24NBJtxyQsc00wxssa8wj40bxHhiBm5lCBk00f3buKj5m";
            _orderService = orderService;
            _authService = authService;
        }

        public async Task<Session> CreateCheckoutSession()
        {
            var products = (await _orderService.GetProductByUser()).Data;
            var lineItems = new List<SessionLineItemOptions>();
            products.ForEach(product => lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = product.ProductPrice * 100,
                    Currency = "try",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.ProductName,
                    }
                },
            }));

            var options = new SessionCreateOptions
            {
                CustomerEmail = _authService.GetUserEmail(),
                ShippingAddressCollection =
                    new SessionShippingAddressCollectionOptions
                    {
                        AllowedCountries = new List<string> { "TR" }
                    },
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = lineItems,
                Mode = "payment",
                //SuccessUrl = "https://localhost:7297/order-success",
                //CancelUrl = "https://localhost:7297/cart"
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return session;
        }
    }
}

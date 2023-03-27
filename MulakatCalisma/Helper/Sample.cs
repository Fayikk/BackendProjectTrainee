using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.EntityFrameworkCore;
using MulakatCalisma.Common;
using MulakatCalisma.Context;
using MulakatCalisma.Services.Abstract;
using NUnit.Framework;
using static System.Net.WebRequestMethods;

namespace MulakatCalisma.Helper
{
    public class Sample : ISample
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;
        public Sample(ApplicationDbContext context,IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }
            
        public string Cancel_Refund()
        {
            Options options = new Options();
            options.ApiKey = Keys.apiKey; //Iyzico Tarafından Sağlanan Api Key
            options.SecretKey = Keys.secretKey; //Iyzico Tarafından Sağlanan Secret Key
            options.BaseUrl = Keys.baseUrl;
            CreateCancelRequest request = new CreateCancelRequest();
            request.ConversationId = "123456788";
            request.Locale = Locale.TR.ToString();
            request.PaymentId = "19029303";
            request.Ip = "85.34.78.112";

            Cancel cancel = Cancel.Create(request, options);
            return null;
        }

        public  string Should_Create_Payment()
        {
            var user = _authService.GetUserId();
            var forEmail = _authService.GetUserEmail();
            var result = _context.Baskets.FirstOrDefault(x=>x.UserId== user);
            var TotalPrices = result.TotalPrice.ToString();

            Options options = new Options();
            options.ApiKey = Keys.apiKey; //Iyzico Tarafından Sağlanan Api Key
            options.SecretKey = Keys.secretKey; //Iyzico Tarafından Sağlanan Secret Key
            options.BaseUrl = Keys.baseUrl;

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456788";
            request.Price = "1";
            request.PaidPrice = "149000.0";
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = "John Doe";
            paymentCard.CardNumber = "5890040000000016";
            paymentCard.ExpireMonth = "12";
            paymentCard.ExpireYear = "2030";
            paymentCard.Cvc = "123";
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = forEmail;
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = "BI101";
            firstBasketItem.Name = "Binocular";
            firstBasketItem.Category1 = "Collectibles";
            firstBasketItem.Category2 = "Accessories";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = "0.3";
            basketItems.Add(firstBasketItem);

            BasketItem secondBasketItem = new BasketItem();
            secondBasketItem.Id = "BI102";
            secondBasketItem.Name = "Game code";
            secondBasketItem.Category1 = "Game";
            secondBasketItem.Category2 = "Online Game Items";
            secondBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            secondBasketItem.Price = "0.5";
            basketItems.Add(secondBasketItem);

            BasketItem thirdBasketItem = new BasketItem();
            thirdBasketItem.Id = "BI103";
            thirdBasketItem.Name = "Usb";
            thirdBasketItem.Category1 = "Electronics";
            thirdBasketItem.Category2 = "Usb / Cable";
            thirdBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            thirdBasketItem.Price = "0.2";
            basketItems.Add(thirdBasketItem);
            request.BasketItems = basketItems;

            Payment payment = Payment.Create(request, options);
            return null;
        }



    }
}

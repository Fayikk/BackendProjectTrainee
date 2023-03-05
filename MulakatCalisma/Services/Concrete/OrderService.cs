using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using MulakatCalisma.Context;
using MulakatCalisma.Entity;
using MulakatCalisma.Services.Abstract;

namespace MulakatCalisma.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;
        private readonly IEmailSender _mailSender;
        private readonly IBasketService _basketService;
        private readonly IUserMoneyService _userMoneyService;
        public OrderService(ApplicationDbContext context
                        , IEmailSender mailSender
                        , IAuthService authService
                        , IMapper mapper
                        , IBasketService basketService
                        , IUserMoneyService userMoneyService)
        {
            _mailSender = mailSender;
            _context = context;
            _authService = authService;
            _basketService = basketService;
            _userMoneyService = userMoneyService;

        }

        public async Task<ServiceResponse<Order>> CreateOrder(Order order)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == order.UserId);
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == order.ProductId);
            if (user == null && product == null)
            {
                return new ServiceResponse<Order>
                {
                    Success = false,
                    Message = "Fail Request",
                };
            }
            else if (user != null && product != null)
            {
                order.ProductName = product.Name;
                order.ProductPrice = product.Price;
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return new ServiceResponse<Order>
                {
                    Message = "Order Succesfully",
                    Success = true,
                };

            }
            return new ServiceResponse<Order>
            {
                Message = "ss",
            };
        }

        public async Task<ServiceResponse<List<Order>>> GetProductByUser()
        {
            var response = await _context.Orders.Where(x => x.UserId == _authService.GetUserId()).ToListAsync();
            if (response != null)
            {
                return new ServiceResponse<List<Order>>
                {
                    Success = true,
                    Data = response,
                };

            }
            return new ServiceResponse<List<Order>> { Success = false, };

        }

        public async Task<ServiceResponse<bool>> Refund(int OrderId)
        {
            var user = _authService.GetUserId();
            var AnyOrder = await _context.Orders.AnyAsync(x => x.UserId == user);
            var response = await _context.Orders.FirstOrDefaultAsync(x => x.Id == OrderId);
            var UserMoney = await _context.UserMoneys.FirstOrDefaultAsync(x => x.UserId == user);
            if (AnyOrder == true)
            {
                response.Status = false;
                _context.Orders.Update(response);
                await _context.SaveChangesAsync();
            }
            if (response.Status == false)
            {
                UserMoney.Money = UserMoney.Money + response.TotalPrice;
                _context.UserMoneys.Update(UserMoney);
                await _context.SaveChangesAsync();
                return new ServiceResponse<bool>
                {
                    Success = true,
                };
            }
            return new ServiceResponse<bool> { Success = false, Message = "Something went wrong" };

        }

        public async Task<ServiceResponse<List<Order>>> StoreCartItem(List<Order> order)
        {
            var result = await _context.Baskets.Where(x => x.UserId == _authService.GetUserId()).ToListAsync();
            var user = _authService.GetUserId();
            var checkMoney = await _context.UserMoneys.FirstOrDefaultAsync(x => x.UserId == user);
            var User = await _context.Users.FirstOrDefaultAsync(x => x.Id == user);
            decimal count = 0;
            if (result == null)
            {
                return new ServiceResponse<List<Order>>
                {
                    Success = true,
                    Message = "You dont have product into the basket",
                };
            }
            foreach (var item in result)
            {
                count = count + item.TotalPrice;
            }

            if (checkMoney.Money < count)
            {
                return new ServiceResponse<List<Order>>
                {
                    Success = false,
                    Message = "Your Money Chip Is Not Price This Product,Im Sorry Poor",
                };
            }

            foreach (var item in result)
            {
                Order deneme = new Order();

                deneme.ProductName = item.ProductName;
                deneme.UserId = item.UserId;
                deneme.ProductId = item.ProductId;
                deneme.ProductPrice = item.Price;
                deneme.Status = true;
                order.Add(deneme);
                await _basketService.DeleteBasket(item);
            }

            _context.Orders.AddRange(order);
            await _userMoneyService.DeleteMoney(count);
            await _context.SaveChangesAsync();
            await _mailSender.SendEmailAsync(User.Email, "Assos Your Order Confirmation:", User.Role);

            return new ServiceResponse<List<Order>>
            {
                Success = true,
                Message = "Your Ordered is Successfully",

            };

        }
    }
}

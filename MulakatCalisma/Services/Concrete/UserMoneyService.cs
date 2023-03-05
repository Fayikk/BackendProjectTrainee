using Microsoft.EntityFrameworkCore;
using MulakatCalisma.Context;
using MulakatCalisma.Entity;
using MulakatCalisma.Services.Abstract;

namespace MulakatCalisma.Services.Concrete
{
    public class UserMoneyService : IUserMoneyService
    {
        private readonly ApplicationDbContext _context;

        private readonly IAuthService _authService;
        public UserMoneyService(IAuthService authService, ApplicationDbContext context)
        {
            _context = context;
            _authService = authService;
        }


        public async Task<ServiceResponse<UserMoney>> AddMoney(UserMoney Money)
        {
            var response = _authService.GetUserId();
            var result = await _context.UserMoneys.FirstOrDefaultAsync(x => x.UserId == Money.UserId);
            if (result != null)
            {
                result.Money += Money.Money;
                _context.UserMoneys.Update(result);
                await _context.SaveChangesAsync();
                return new ServiceResponse<UserMoney>
                {
                    Message = $"You Chip Money Is = {result.Money}",
                    Success = false,
                };
            }
            else
            {
                _context.UserMoneys.Add(Money);
                await _context.SaveChangesAsync();
                return new ServiceResponse<UserMoney>
                {
                    Message = $"Add Your Chip Account = {Money.Money}",
                    Success = true,
                };
            }



        }

        public async Task<ServiceResponse<UserMoney>> DeleteMoney(decimal price)
        {
            var user = _authService.GetUserId();
            var response = await _context.UserMoneys.FirstOrDefaultAsync(x => x.UserId == user);
            if (response != null)
            {

              
                response.Money = response.Money - price;
                _context.UserMoneys.Update(response);
                await _context.SaveChangesAsync();
                return new ServiceResponse<UserMoney>
                {
                    Data = response,
                    Success = true,
                };
            }
            else
            {
                return new ServiceResponse<UserMoney>
                {
                    Message = "Something went wrong",
                    Success = false,
                };
            }

        }

    }
}

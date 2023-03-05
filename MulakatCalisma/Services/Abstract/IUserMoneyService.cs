using MulakatCalisma.Entity;

namespace MulakatCalisma.Services.Abstract
{
    public interface IUserMoneyService
    {
        Task<ServiceResponse<UserMoney>> AddMoney(UserMoney Money);
        Task<ServiceResponse<UserMoney>> DeleteMoney(decimal price);
    }
}

using MulakatCalisma.DTO;
using MulakatCalisma.Entity;

namespace MulakatCalisma.Services.Abstract
{
    public interface IOrderService
    {
        Task<ServiceResponse<Order>> CreateOrder(Order order);
        Task<ServiceResponse<List<Order>>> GetProductByUser(int userId);
    }
}

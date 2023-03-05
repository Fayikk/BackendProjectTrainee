using MulakatCalisma.DTO;
using MulakatCalisma.Entity;

namespace MulakatCalisma.Services.Abstract
{
    public interface IOrderService
    {
        Task<ServiceResponse<Order>> CreateOrder(Order order);
        Task<ServiceResponse<List<Order>>> GetProductByUser();
        Task<ServiceResponse<List<Order>>> StoreCartItem(List<Order> order);
        Task<ServiceResponse<bool>> Refund(int OrderId);
    }
}

using App.Core.Entities;
using App.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Interfaces.Services
{
    public interface IOrderService : IGenericService<Order>
    {
        Task<IQueryable<OrderModel>> GetAllOrdersBaseModels();
        Task<IQueryable<OrderModel>> GetAllOrders();
        Task<OrderModel> GetOrderById(long orderId);
        Task<OrderModel> GetBaseOrderById(long orderId);
        Task<IQueryable<OrderModel>> GetUserOrders(string userId);
        Task<OrderModel> AddOrder(OrderModel model);
        Task<OrderModel> EditOrder(OrderModel model);
        Task<BooleanDescriptionResultDTO> CancelOrder(long orderId );

    }
}

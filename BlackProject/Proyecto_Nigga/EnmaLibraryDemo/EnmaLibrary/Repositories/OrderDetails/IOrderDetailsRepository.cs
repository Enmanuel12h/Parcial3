using EnmaLibrary.Models;

namespace EnmaLibrary.Repositories
{
    public interface IOrderDetailsRepository
    {
        Task AddAsync(OrderDetailsModel orderDetails);
        Task DeleteAsync(int id);
        Task EditAsync(OrderDetailsModel orderDetails);
        Task<IEnumerable<OrderDetailsModel>> GetAllAsync(int orderId);
        Task<OrderDetailsModel?> GetByIdAsync(int id);
        Task<IEnumerable<OrderDetailsModel>> GetOrderDetailsByOrderIdAsync(int orderId);
    }
}
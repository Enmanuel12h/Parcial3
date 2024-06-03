using EnmaLibrary.Models;

namespace EnmaLibrary.Repositories
{
    public interface IOrdersRepository
    {
        Task<int> AddAsync(OrdersModel order);
        Task DeleteAsync(int id);
        Task EditAsync(OrdersModel order);
        Task<IEnumerable<OrdersModel>> GetAllAsync();
        Task<OrdersModel?> GetByIdAsync(int id);
        Task<IEnumerable<OrderDetailsModel>> GetSpecificByIdAsync(int id);
    }
}
using EnmaLibrary.Data;
using EnmaLibrary.Models;
using System.Data;

namespace EnmaLibrary.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public OrdersRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<int> AddAsync(OrdersModel order)
        {
            var parameters = new
            {
                order.CustomerId,
                order.OrderDate,
                order.Status
            };

            int newOrderId = await _dataAccess.SaveDataOrderAsync(
                "dbo.spOrders_Insert",
                parameters
            );

            return newOrderId;
        }

        public async Task DeleteAsync(int id)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spOrders_Delete",
                new { Id = id }
            );
        }

        public async Task EditAsync(OrdersModel order)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spOrders_Update",
                new
                {
                    order.Id,
                    order.CustomerId,
                    order.OrderDate,
                    order.Status
                });
        }

        public async Task<IEnumerable<OrdersModel>> GetAllAsync()
        {
            var orders = await _dataAccess.GetDataAsync<OrdersModel, dynamic>(
                "dbo.spOrders_GetAll",
                new { }
            );

            return orders;
        }

        public async Task<OrdersModel?> GetByIdAsync(int id)
        {
            var order = await _dataAccess.GetDataAsync<OrdersModel, dynamic>(
                "dbo.spOrders_GetById",
                new { Id = id }
            );

            return order.FirstOrDefault();
        }

        public async Task<IEnumerable<OrderDetailsModel>> GetSpecificByIdAsync(int id)
        {
            string storedProcedure = "spOrderDetails_GetAllByOrderId";

            var orderDetails = await _dataAccess.GetDataAsync<OrderDetailsModel, dynamic>(
                storedProcedure,
                new { OrderId = id },
                commandType: CommandType.StoredProcedure
            );

            return orderDetails;
        }

    }
}

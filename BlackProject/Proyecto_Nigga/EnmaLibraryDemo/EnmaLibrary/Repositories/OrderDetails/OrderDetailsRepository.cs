using EnmaLibrary.Data;
using EnmaLibrary.Models;

namespace EnmaLibrary.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public OrderDetailsRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task AddAsync(OrderDetailsModel orderDetails)
        {
            var parameters = new
            {
               orderDetails.OrderId,
               orderDetails.BookId,
               orderDetails.Quantity,
               orderDetails.UnitPrice,
               orderDetails.TotalPrice
            };

            await _dataAccess.SaveDataAsync(
                "dbo.spOrderDetails_Insert",
                parameters
            );
        }

        public async Task EditAsync(OrderDetailsModel orderDetails)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spOrderDetails_Update",
                new
                {
                    orderDetails.Id,
                    orderDetails.OrderId,
                    orderDetails.BookId,
                    orderDetails.Quantity,
                    orderDetails.UnitPrice,
                    orderDetails.TotalPrice
                });
        }


        public async Task DeleteAsync(int id)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spOrderDetails_Delete",
                new { Id = id }
            );
        }

        public async Task<IEnumerable<OrderDetailsModel>> GetAllAsync(int orderId)
        {
            var orderDetails = await _dataAccess.GetDataAsync<OrderDetailsModel, dynamic>(
                "dbo.spOrderDetails_GetAll",
                new { OrderId = orderId }
            );

            return orderDetails;
        }

        public async Task<OrderDetailsModel?> GetByIdAsync(int id)
        {
            var orderDetails = await _dataAccess.GetDataAsync<OrderDetailsModel, dynamic>(
                "spOrderDetails_GetById",
                new { Id = id }
            );

            return orderDetails.FirstOrDefault();
        }

        public async Task<IEnumerable<OrderDetailsModel>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            var orderDetails = await _dataAccess.GetDataAsync<OrderDetailsModel, dynamic>(
                "dbo.spOrderDetails_GetAllByOrderId",
                new { OrderId = orderId }
            );

            return orderDetails;
        }
    }
}

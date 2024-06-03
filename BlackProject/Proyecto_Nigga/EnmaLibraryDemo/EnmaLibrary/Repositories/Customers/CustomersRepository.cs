using EnmaLibrary.Data;
using EnmaLibrary.Models;

namespace EnmaLibrary.Repositories.Customers
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public CustomersRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<CustomersModel>> GetAllCustomersAsync()
        {
            return await _dataAccess.GetDataAsync<CustomersModel, dynamic>(
                "dbo.spCustomers_GetAll",
                new { });
        }

        public async Task<CustomersModel> GetCustomerByIdAsync(int id)
        {
            var customers = await _dataAccess.GetDataAsync<CustomersModel, dynamic>(
                "dbo.spCustomers_GetById",
                new { Id = id });

            return customers.FirstOrDefault();
        }

        public async Task AddCustomerAsync(CustomersModel customer)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spCustomers_Insert",
                new { customer.Name, customer.Email, customer.Address, customer.Phone });
        }

        public async Task UpdateCustomerAsync(CustomersModel customer)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spCustomers_Update",
                customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spCustomers_Delete",
                new { Id = id });
        }
    }
}

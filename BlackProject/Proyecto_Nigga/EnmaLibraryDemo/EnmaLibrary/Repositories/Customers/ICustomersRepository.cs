using EnmaLibrary.Models;

namespace EnmaLibrary.Repositories.Customers
{
    public interface ICustomersRepository
    {
        Task AddCustomerAsync(CustomersModel customer);
        Task DeleteCustomerAsync(int id);
        Task<IEnumerable<CustomersModel>> GetAllCustomersAsync();
        Task<CustomersModel> GetCustomerByIdAsync(int id);
        Task UpdateCustomerAsync(CustomersModel customer);
    }
}
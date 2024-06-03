using EnmaLibrary.Models;

namespace EnmaLibrary.Repositories.Users
{
    public interface IUsersRepository
    {
        Task AddUserAsync(UsersModel user);
        Task DeleteUserAsync(int id);
        Task<IEnumerable<UsersModel>> GetAllUsersAsync();
        Task<UsersModel> GetUserByIdAsync(int id);
        Task UpdateUserAsync(UsersModel user);
    }
}
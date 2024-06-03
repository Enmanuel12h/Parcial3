using EnmaLibrary.Data;
using EnmaLibrary.Models;

namespace EnmaLibrary.Repositories.Users
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public UsersRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<UsersModel>> GetAllUsersAsync()
        {
            return await _dataAccess.GetDataAsync<UsersModel, dynamic>(
                "dbo.spUsers_GetAll",
                new { });
        }

        public async Task<UsersModel> GetUserByIdAsync(int id)
        {
            var users = await _dataAccess.GetDataAsync<UsersModel, dynamic>(
                "dbo.spUsers_GetById",
                new { Id = id });

            return users.FirstOrDefault();
        }

        public async Task AddUserAsync(UsersModel user)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spUsers_Insert",
                new { user.Name, user.Email, user.Password, user.RegistrationDate });
        }

        public async Task UpdateUserAsync(UsersModel user)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spUsers_Update",
                user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spUsers_Delete",
                new { Id = id });
        }
    }
}

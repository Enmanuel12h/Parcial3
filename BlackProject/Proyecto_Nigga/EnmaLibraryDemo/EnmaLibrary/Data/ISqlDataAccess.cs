
using System.Data;

namespace EnmaLibrary.Data
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> GetDataAsync<T, P>(
        string storedProcedure,
        P parameters,
        CommandType commandType = CommandType.StoredProcedure,
        string connection = "default");
        Task<IEnumerable<T>> GetDataForeignAsync<T, U, V, P>(string storedProcedure, P parameters, Func<T, U, V, T>? map = null, string connection = "default", string splitOn = "Id");
        Task SaveDataAsync<T>(string storedProcedure, T parameters, string connection = "default");
        Task<int> SaveDataOrderAsync<T>(string storedProcedure, T parameters, string connection = "default");
    }
}
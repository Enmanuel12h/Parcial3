using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace EnmaLibrary.Data
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _configuration;

        public SqlDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<T>> GetDataAsync<T, P>(
        string storedProcedure,
        P parameters,
        CommandType commandType = CommandType.StoredProcedure,
        string connection = "default")
        {
            using IDbConnection dbConnection =
                new SqlConnection(_configuration.GetConnectionString(connection));

            return await dbConnection.QueryAsync<T>(
                storedProcedure,
                parameters,
                commandType: commandType);
        }

        public async Task SaveDataAsync<T>(
            string storedProcedure,
            T parameters,
            string connection = "default")
        {
            using IDbConnection dbConnection =
                new SqlConnection(_configuration.GetConnectionString(connection));

            await dbConnection.ExecuteAsync(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> SaveDataOrderAsync<T>(
        string storedProcedure,
        T parameters,
        string connection = "default")
        {
            using IDbConnection dbConnection =
                new SqlConnection(_configuration.GetConnectionString(connection));

            var dynamicParameters = new DynamicParameters(parameters);
            dynamicParameters.Add("NewOrderId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await dbConnection.ExecuteAsync(
                storedProcedure,
                dynamicParameters,
                commandType: CommandType.StoredProcedure);

            int newOrderId = dynamicParameters.Get<int>("NewOrderId");
            return newOrderId;
        }



        public async Task<IEnumerable<T>> GetDataForeignAsync<T, U, V, P>(
            string storedProcedure,
            P parameters,
            Func<T, U, V, T>? map = null,
            string connection = "default",
            string splitOn = "Id")
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString(connection));

            if (map == null)
            {
                return await dbConnection.QueryAsync<T>(
                    storedProcedure,
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            else
            {
                return await dbConnection.QueryAsync<T, U, V, T>(
                    storedProcedure,
                    map,
                    parameters,
                    splitOn: splitOn,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}

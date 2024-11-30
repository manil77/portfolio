using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.SQLHelper
{
    public class SQLHelper : ISQLHelper
    {
        private readonly IConfiguration _configuration;
        public SQLHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string GetConnectionString(bool useReportDB = false)
        {
            try
            {
                string key = useReportDB ? "ReportDBConnString" : "DefaultConnection";

                //return Environment.GetEnvironmentVariable(key) ?? _configuration.GetValue<string>(key);
                return _configuration.GetConnectionString(key) ?? _configuration["ConnectionStrings:DefaultConnection"]; ;
            }
            catch
            {
                return _configuration["ConnectionStrings:DefaultConnection"];
            }
        }

        private string GetLogConnectionString()
        {
            try
            {
                string key = "LogDBConnString";
                return _configuration.GetConnectionString(key) ?? _configuration["ConnectionStrings:DefaultConnection"]; 

            }
            catch
            {
                return _configuration["ConnectionStrings:DefaultConnection"];
            }
        }

        #region Dapper Calls
        public IEnumerable<T> ExecuteStoredProcedure<T>(string procedureName, object parameters = null, int? commandTimeout = null)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                var result = connection.Query<T>(
                    procedureName,                 // Stored procedure name
                    parameters,                    // Parameters (can be null if none)
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: commandTimeout
                ).ToList();
                return result;
            }
        }

        public IEnumerable<T> ExecuteSqlScript<T>(
            string sqlScript,
            object parameters = null,
            int? commandTimeout = null)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                var result = connection.Query<T>(
                    sqlScript,                // SQL script to execute
                    parameters,               // Parameters (if any)
                    commandType: CommandType.Text, // CommandType.Text for raw SQL
                    commandTimeout: commandTimeout  // Optional timeout
                ).ToList();

                return result;
            }
        }
        #endregion
    }
}

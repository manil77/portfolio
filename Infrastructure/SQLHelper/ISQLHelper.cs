namespace Infrastructure.SQLHelper
{
    public interface ISQLHelper
    {
        IQueryable<T> ExecuteStoredProcedure<T>(string procedureName, object parameters = null, int? commandTimeout = null);
        IQueryable<T> ExecuteSqlScript<T>(string sqlScript, object parameters = null, int? commandTimeout = null);
        void ExecuteRawSqlScript(string sqlScript, int? commandTimeout = null);
    }
}
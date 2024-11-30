namespace Infrastructure.SQLHelper
{
    public interface ISQLHelper
    {
        IEnumerable<T> ExecuteStoredProcedure<T>(string procedureName, object parameters = null, int? commandTimeout = null);
        IEnumerable<T> ExecuteSqlScript<T>(string sqlScript, object parameters = null, int? commandTimeout = null);
    }
}
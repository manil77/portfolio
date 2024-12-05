using Dapper;
using System.Data;

namespace Portfolio.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDbConnection _dbConnection;

        public GlobalExceptionMiddleware(RequestDelegate next/*, ISQLHelper sqlHelper */, IDbConnection dbconnection)
        {
            _next = next;
            //_sqlHelper = sqlHelper;
            _dbConnection = dbconnection;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            string currentURL = context.Request.Host.ToString() + context.Request.Path;


            string sql = $"insert into error_log(url, exception_message, stack_trace, timestamp) values " +
                         $"('{currentURL}','{exception.Message}', '{exception.StackTrace}'," +
                         $" '{DateTime.UtcNow}');";

            

            //_sqlHelper.ExecuteRawSqlScript(sql);
            await _dbConnection.ExecuteAsync(sql, new
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace,
                Timestamp = DateTime.UtcNow
            });

            //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //context.Response.ContentType = "application/json";

            //await context.Response.WriteAsync(new
            //{
            //    StatusCode = context.Response.StatusCode,
            //    Message = "An unexpected error occurred. Please try again later."
            //}.ToString());

        }

    }
}

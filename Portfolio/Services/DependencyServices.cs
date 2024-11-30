using Application.Interfaces.UnitOfWork;
using Application.Services.UnitOfWork;
using Infrastructure.Interfaces.UnitOfWork;
using Infrastructure.Repository.UnitOfWork;
using Infrastructure.SQLHelper;

namespace Middleware.Services
{
    public static class DependencyServices
    {
        public static IServiceCollection AddDependencyServices(this IServiceCollection services) {
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IRepositoryUnitOfWork, RepositoryUnitOfWork>();
            services.AddScoped<ISQLHelper, SQLHelper>();

            return services;
        }
    }
}

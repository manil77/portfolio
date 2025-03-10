﻿using Application.ApplicationInterfaces.UnitOfWork;
using Application.Services.UnitOfWork;
using Application.RepositoryInterfaces.UnitOfWork;
using Infrastructure.Repository.UnitOfWork;
using Infrastructure.SQLHelper;
using Infrastructure.Services;


namespace Middleware.Services
{
    public static class DependencyServices
    {
        public static IServiceCollection AddDependencyServices(this IServiceCollection services) {
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IRepositoryUnitOfWork, RepositoryUnitOfWork>();
            services.AddScoped<ISQLHelper, SQLHelper>();
            services.AddScoped<JWTAuthService>();
            services.AddScoped<SignInManagerService>();

            return services;
        }
    }
}

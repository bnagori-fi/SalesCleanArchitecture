using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Contracts.Infrastructure;
using Sales.Application.Contracts.Persistence;
using Sales.Infrastructure.Persistence;
using Sales.Infrastructure.Persistence.Repositories;
using Sales.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<SalesDbContext>(options =>
                            options.UseSqlServer(configuration.GetConnectionString("DbConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<,>), typeof(RepositoryBase<,>));
            services.AddScoped<ISalesRepository, SalesRepository>();

            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();


            return services;
        }
    }
}

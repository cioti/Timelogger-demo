using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Timelogger.Domain.Abstractions;
using Timelogger.Infrastructure.Data;
using Timelogger.Infrastructure.Repositories;

namespace Timelogger.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<ApiDbContext>(opts => opts.UseSqlite(connectionString,
                opts => opts.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));
            services.AddScoped(typeof(IGenericAsyncRepository<>),typeof(GenericAsyncRepository<>));
            return services;
        }
    }
}

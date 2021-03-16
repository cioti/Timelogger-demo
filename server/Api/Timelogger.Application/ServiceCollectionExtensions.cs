using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Timelogger.Application.Commands.Projects;

namespace Timelogger.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateProjectCommandHandler).Assembly);
            return services;
        }
    }
}

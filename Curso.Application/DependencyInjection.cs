using Microsoft.Extensions.DependencyInjection;
using Curso.Application.Interfaces;
using Curso.Application.Services;

namespace Curso.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register application services here
            services.AddScoped<ITipoClienteService, TipoClienteService>();
            return services;
        }
    }
}

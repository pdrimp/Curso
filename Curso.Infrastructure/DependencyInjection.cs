using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Curso.Application.Interfaces;
using Curso.Infrastructure.Repositories;
using Curso.Infrastructure.Data;

namespace Curso.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register infrastructure services here
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ?? "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CursoDb;Integrated Security=True"));

            services.AddScoped<ITipoClienteRepository, TipoClienteRepository>();
            services.AddScoped<IInteresRepository, InteresRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            return services;
        }
    }
}

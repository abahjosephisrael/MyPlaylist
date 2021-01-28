using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;

namespace Persistence
{
    /// <summary>
    /// This class registers all services for easy and clean registration in the Startup.cs class.
    /// Ensure that all services are registered here.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// A static method for service registration in the Startup.cs Class.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("MyPlaylistDBConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()); 
        }
    }
}

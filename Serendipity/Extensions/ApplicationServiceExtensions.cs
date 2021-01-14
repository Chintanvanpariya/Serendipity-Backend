using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serendipity.Data;
using Serendipity.Interfaces;
using Serendipity.Services;

namespace Serendipity.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
        {

            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
            );

            return services;
        }

    }
}

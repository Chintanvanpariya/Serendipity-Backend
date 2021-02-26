using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serendipity.Data;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Serendipity.Entities;

namespace Serendipity
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DataContext>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
                await context.Database.MigrateAsync();
                await Seed.SeedUsers(userManager, roleManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during migration");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}


//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            CreateHostBuilder(args).Build().Run();
//            //var host = CreateHostBuilder(args).Build();

//            //using var scope = host.Services.CreateScope();
//            //var services = scope.ServiceProvider;
//            //try
//            //{
//            //    var context = services.GetRequiredService<DataContext>();
//            //    await context.Database.MigrateAsync();
//            //    await Seed.SeedUser(context);
//            //}
//            //catch (Exception ex)
//            //{
//            //    var logger = services.GetRequiredService<ILogger<Program>>();
//            //    logger.LogError(ex, "An error occured during migration");

//            //}

//            //await host.RunAsync();
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)
//                .ConfigureWebHostDefaults(webBuilder =>
//                {
//                    webBuilder.UseStartup<Startup>();
//                });
//    }
//}

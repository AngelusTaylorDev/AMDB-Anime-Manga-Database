using AMDB_Anime_Manga_Database.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMDB_Anime_Manga_Database
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // OLD - CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();

            var dataService = host.Services.CreateScope().ServiceProvider.GetRequiredService<SeedService>();

            // Call to the ManageDataAsync method
            await dataService.ManageDataAsync();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

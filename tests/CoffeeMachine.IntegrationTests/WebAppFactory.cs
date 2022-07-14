using System.Collections.Generic;
using System.Linq;

using CoffeeMachine.Infrastructure;
using CoffeeMachine.Web;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeMachine.IntegrationTests
{
    public class WebAppFactory :
        WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType ==
                                                               typeof(DbContextOptions<DataContext>));

                services.Remove(descriptor);
                services.AddDbContext<DataContext>(opt => { opt.UseInMemoryDatabase("testDb"); });
            });

            builder.ConfigureAppConfiguration((context, config) =>
            {
                var appSettings = new Dictionary<string, string>
                {
                    { "Jwt:Key", "111111111111111111111111" },
                    { "Jwt:Issuer", "Test" },
                    { "Jwt:ExpirationTime", "30" }
                };

                config.AddInMemoryCollection(appSettings).Build();
            });
        }
    }
}
using System;
using System.Linq;

using CoffeeMachine.Infrastructure;
using CoffeeMachine.Web;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
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
                services.AddDbContext<DataContext>(opt =>
                {
                    opt.UseInMemoryDatabase("testDb");
                });
            });
        }
    }
}
using DataAccess.Models;
using Interface.Factories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;

namespace Test.Helpers;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the actual DbContext
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<dbo>));
            if (dbContextDescriptor != null)
            {
                services.Remove(dbContextDescriptor);
            }
            
            services.AddDbContext<dbo>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });
            
            var mockLogicFactoryBuilder = new Mock<ILogicFactoryBuilder>();
            var mockDalFactory = new Mock<IDalFactory>();

            services.Replace(new ServiceDescriptor(typeof(ILogicFactoryBuilder), mockLogicFactoryBuilder.Object));
            services.Replace(new ServiceDescriptor(typeof(IDalFactory), mockDalFactory.Object));
        });
    }
}
using MediscreenAPI.Model;
using MediscreenWepApp;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MediscreenAPI.tests.FunctionalTests
{
    public class FunctionalTests
    {
        protected readonly HttpClient TestClient;
        protected FunctionalTests()
        {
            //This create a mirror application, at the exception of using in memory db
            WebApplicationFactory<Startup> appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        Microsoft.Extensions.DependencyInjection.ServiceDescriptor? descriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(DbContextOptions<PatientContext>));
                        services.Remove(descriptor);
                        services.AddDbContext<PatientContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                    });
                });

            TestClient = appFactory.CreateClient();
        }
    }
}

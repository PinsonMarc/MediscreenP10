using MediscreenAPI.Model;
using Microsoft.EntityFrameworkCore;
using PoseidonApi.Services;

namespace MediscreenWepApp;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.ConfigureAutoMapper();
        services.AddDbContext<PatientContext>(
            options => options.UseSqlServer(Configuration.GetConnectionString("PatientContext"))
        );
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}

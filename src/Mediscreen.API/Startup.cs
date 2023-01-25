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
        services.AddSwaggerGen(c =>
        {
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });

        services.ConfigureAutoMapper();
        //SQL
        services.AddDbContext<PatientContext>(
            options => options.UseSqlServer(Configuration.GetConnectionString("PatientContext"))
        );
        //NoSql
        services.ConfigureMongoDb(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        using (IServiceScope scope = app.ApplicationServices.CreateScope())
        {
            using PatientContext? context = scope.ServiceProvider.GetService<PatientContext>();
            if (context != null) context.Database.EnsureCreated();
        }

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mediscreen API V1");
            });
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

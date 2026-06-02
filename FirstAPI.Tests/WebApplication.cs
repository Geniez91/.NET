using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

public class ApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            using var scope = services.BuildServiceProvider().CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

           db.Database.EnsureDeleted();
           db.Database.EnsureCreated();
        });
    }
}
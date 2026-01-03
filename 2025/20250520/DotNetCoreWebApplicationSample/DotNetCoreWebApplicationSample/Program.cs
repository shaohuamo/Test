using DotNetCoreWebApplicationSample.Repositories;
using DotNetCoreWebApplicationSample.RepositoryContracts;
using DotNetCoreWebApplicationSample.ServiceContracts;
using DotNetCoreWebApplicationSample.Services;

namespace DotNetCoreWebApplicationSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddScoped<ICustomLogger, CustomLogger>();
            builder.Services.AddScoped<IOrderRepository,OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");
            app.MapControllers();
            app.Run();
        }
    }
}

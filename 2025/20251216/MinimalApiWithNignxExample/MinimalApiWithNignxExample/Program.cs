var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHsts();
app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    if (app.Environment.IsDevelopment())
    {
        //用于debug时多实例的测试
        string? instanceId = app.Configuration["INSTANCE_ID"];
        Console.WriteLine($"Request served by {instanceId}");
    }
    //用于docker中多实例的测试
    string? appName = app.Configuration["APP_NAME"] ?? "default app";
    Console.WriteLine($"Request served by {appName}");
    
    return "Hello World!";
});

app.Run();

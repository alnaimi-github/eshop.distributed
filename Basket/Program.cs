using ServiceDefaults.Messaging;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddRedisDistributedCache(connectionName: "cache");

builder.Services.AddScoped<BasketService>();

builder.Services.AddHttpClient<CatalogApiClient>(client =>
{
    client.BaseAddress = new Uri("https+http://catalog");
});

builder.Services.AddMassTransitWithAssemblies(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseHttpsRedirection();

app.MapDefaultEndpoints();

app.MapBasketEndpoints();

app.Run();


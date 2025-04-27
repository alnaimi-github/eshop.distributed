var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ProductDbContext>(connectionName: "catalogdb");

builder.Services.AddScoped<ProductService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseMigration();

app.UseHttpsRedirection();

app.MapProductEndpoints();

app.MapDefaultEndpoints();

app.MapControllers();

app.Run();

using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ProductDbContext>(connectionName: "catalogdb");

builder.Services.AddControllers();

var app = builder.Build();

app.UseMigration();

app.UseHttpsRedirection();

app.MapDefaultEndpoints();

app.MapControllers();

app.Run();

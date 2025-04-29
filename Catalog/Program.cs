using ServiceDefaults.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ProductDbContext>(connectionName: "catalogdb");

builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<ProductAIService>();

builder.Services.AddMassTransitWithAssemblies(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();

// Register Ollama-based chat & embedding
builder.AddOllamaSharpChatClient("ollama-llama3-2");

var app = builder.Build();

app.UseMigration();

app.UseHttpsRedirection();

app.MapProductEndpoints();

app.MapDefaultEndpoints();

app.MapControllers();

app.Run();

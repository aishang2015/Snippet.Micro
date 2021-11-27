using Snippet.Micro.Consul;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddConsulConfig(builder.Configuration.GetSection("Consul"));
builder.Services.AddConsulRegisterService();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

using Snippet.Micro.Consul;
using Snippet.Micro.Operation.Api;
using Snippet.Micro.Rbac.Api;
using Snippet.Micro.Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.AddConsulConfiguraion()     // 配置中心
    .AddConsulRegisterService()     // 服务注册
    .AddFileLog();                  // 日志
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 配置服务间调用
builder.Services
    .AddOperationApi()
    .AddRbacApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

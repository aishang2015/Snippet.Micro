using Snippet.Micro.Consul;
using Snippet.Micro.Operation.Api;
using Snippet.Micro.Rbac.Api;
using Snippet.Micro.Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.AddConsulConfiguraion()     // ��������
    .AddConsulRegisterService()     // ����ע��
    .AddFileLog();                  // ��־
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ���÷�������
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

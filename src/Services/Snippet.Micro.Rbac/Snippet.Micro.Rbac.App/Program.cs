using Snippet.Micro.Common.Extensions;
using Snippet.Micro.Common.Midlewares;
using Snippet.Micro.Consul;
using Snippet.Micro.EntityFrameworkCore;
using Snippet.Micro.Rbac.App.Data;
using Snippet.Micro.Rbac.App.Data.Entity.RBAC;
using Snippet.Micro.Rbac.App.Extensions;
using Snippet.Micro.Rbac.App.Models;
using Snippet.Micro.RBAC.Core.Middleware;
using Snippet.Micro.RBAC.Data;
using Snippet.Micro.Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.AddConsulConfiguraion()     // ��������
    .AddConsulRegisterService()     // ����ע��
    .AddFileLog();                  // ��־

builder.Services.AddCustomSwaggerGen()                  // swagger
    .AddHttpContextAccessor()                           // httpcontext
    .AddAutoMapper(typeof(AutoMapperProfile))           // auto mapper
    .AddControllers();

builder.Services
    .AddIdentityDatabase<SnippetAdminDbContext, SnippetAdminUser, SnippetAdminRole, int>
    (builder.Configuration, "DatabaseOption");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        //c.IndexStream = () => Assembly.GetExecutingAssembly().GetManifestResourceStream("SnippetAdmin.Swagger.index.html");
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Snippet.Micro.Rbac v1");
    });
}

app.UseCustomExceptionHandler()     // �쳣����
   .UseHeaderToAuth()               // ������֤ͷת�û���Ϣ
   .UseRouting()                    // ʹ��·��
   .UseEndpoints(endpoints =>       // �˵�ƥ��
   {
       endpoints.MapControllers();
   });

app.Initialize(DbContextInitializer.InitialSnippetAdminDbContext);

app.Run();

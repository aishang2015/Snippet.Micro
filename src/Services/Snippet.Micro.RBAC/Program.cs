
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Snippet.Micro.Consul;
using Snippet.Micro.RBAC.Business;
using Snippet.Micro.RBAC.Core;
using Snippet.Micro.RBAC.Core.Middleware;
using Snippet.Micro.RBAC.Core.Oauth;
using Snippet.Micro.RBAC.Core.TextJson;
using Snippet.Micro.RBAC.Core.UserAccessor;
using Snippet.Micro.RBAC.Data;
using Snippet.Micro.RBAC.Models;
using Snippet.Micro.Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// consulע��
builder.Services.AddConsulConfig(builder.Configuration.GetSection("Consul"));
builder.Services.AddConsulRegisterService();

// �ֱ������ݿ� ���� �ڴ滺�� jwt automapper oauth �û�������
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddOauth(builder.Configuration);
builder.Services.AddUserAccessor();

// ����FluentValidation���ı�Ĭ��modelstate�ķ�����ʽ
builder.Services.AddControllers().AddFluentValidation().AddTextJsonOptions();
builder.Services.ConfigureApiBehavior();

// ���signalr
builder.Services.AddSignalR();

// ����swaggerȨ�޷���
builder.Services.AddCustomSwaggerGen();

// �������ã�����������վ����
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .SetIsOriginAllowed(o => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// httpcontext����
builder.Services.AddHttpContextAccessor();

// ��־
builder.Host.AddElasticsearchLog();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = "http://localhost:10000/identity";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = "rbacapi",
            ValidateIssuer = false,
        };
    });

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.IndexStream = () => Assembly.GetExecutingAssembly().GetManifestResourceStream("SnippetAdmin.Swagger.index.html");
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SnippetAdmin v1");
    });
}

// serilog�ṩ��һ��������¼������Ϣ����־�м������������Ļ�����Ϣ�ᱻ�������־��
app.UseCustomSerilogRequestLogging(500, "/broadcast");

// �����쳣
app.UseCustomExceptionHandler();

// ʹ�ÿ�������
app.UseCors("CorsPolicy");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// ����signalr·��
app.UseEndpoints(endpoints =>
{
    endpoints.MapHubs();
    endpoints.MapControllers();
});

app.Initialize(DbContextInitializer.InitialSnippetAdminDbContext);
app.Run();
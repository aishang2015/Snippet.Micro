
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

// consul注册
builder.Services.AddConsulConfig(builder.Configuration.GetSection("Consul"));
builder.Services.AddConsulRegisterService();

// 分别是数据库 缓存 内存缓存 jwt automapper oauth 用户访问器
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddOauth(builder.Configuration);
builder.Services.AddUserAccessor();

// 配置FluentValidation并改变默认modelstate的返回形式
builder.Services.AddControllers().AddFluentValidation().AddTextJsonOptions();
builder.Services.ConfigureApiBehavior();

// 添加signalr
builder.Services.AddSignalR();

// 配置swagger权限访问
builder.Services.AddCustomSwaggerGen();

// 跨域配置，允许所有网站访问
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .SetIsOriginAllowed(o => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// httpcontext服务
builder.Services.AddHttpContextAccessor();

// 日志
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

// serilog提供的一个用来记录请求信息的日志中间件，所有请求的基本信息会被输出到日志中
app.UseCustomSerilogRequestLogging(500, "/broadcast");

// 处理异常
app.UseCustomExceptionHandler();

// 使用跨域配置
app.UseCors("CorsPolicy");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// 配置signalr路径
app.UseEndpoints(endpoints =>
{
    endpoints.MapHubs();
    endpoints.MapControllers();
});

app.Initialize(DbContextInitializer.InitialSnippetAdminDbContext);
app.Run();
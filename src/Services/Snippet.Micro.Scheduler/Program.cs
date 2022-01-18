using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MongoDB.Driver;
using Snippet.Micro.Consul;
using Snippet.Micro.Serilog;

var builder = WebApplication.CreateBuilder(args);

// 使用consul配置
builder.AddConsulConfiguraion();

// 注册服务
builder.Services.AddConsulRegisterService(builder.Configuration.GetSection("Consul"));

// 使用elk日志
//builder.Host.AddElasticsearchLog();

// 使用认证中心
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration["Authority"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = "schedulerApi",
            ValidateIssuer = false,
        };
    });

var mongoUrlBuilder = new MongoUrlBuilder(builder.Configuration["Mongo:ConnectionString"]);
var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());
builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseMongoStorage(mongoClient, mongoUrlBuilder.DatabaseName, new MongoStorageOptions
        {
            MigrationOptions = new MongoMigrationOptions
            {
                MigrationStrategy = new MigrateMongoMigrationStrategy(),
                BackupStrategy = new CollectionMongoBackupStrategy()
            },
            Prefix = "hangfire.mongo",
            CheckConnection = true ,
            CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection
        })
    );

// Add the processing server as IHostedService
builder.Services.AddHangfireServer(serverOptions =>
{
    serverOptions.ServerName = "Snippet.Micro.Scheduler Hangfire Server";
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHangfireDashboard(); 
BackgroundJob.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

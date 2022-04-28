using Microsoft.AspNetCore.Authentication.JwtBearer;
using Snippet.Micro.Common.Midlewares;
using Snippet.Micro.Consul;
using Snippet.Micro.MassTransit;
using Snippet.Micro.Serilog;
using Snippet.Micro.TestService.Consumers;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.AddConsulRegisterService();

//builder.Services.AddRabbitMassTransit(builder.Configuration.GetSection("Rabbit"), new List<Type>
//{
//    typeof(TestConsumer),typeof(GoodConsumer)
//});

//builder.Services
//    .AddAuthentication(options =>
//    {
//        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    })
//    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
//    {
//        options.Authority = "http://localhost:20000/identity";
//        options.RequireHttpsMetadata = false;
//        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//        {
//            ValidateAudience = true,
//            ValidAudience = "testapi",
//            ValidateIssuer = false,
//            //ValidateAudience = false,
//            //ValidIssuer = "http://192.168.0.147:10000/identity",
//            //ValidateAudience = true,
//            //ValidAudience = "http://192.168.0.147:10000/identity/resources",
//            //ValidateIssuerSigningKey = true,
//        };
//        //options.MetadataAddress = "http://192.168.0.147:10000/identity/.well-known/openid-configuration";

//        options.Events = new JwtBearerEvents()
//        {
//            OnTokenValidated = c =>
//            {
//                return Task.CompletedTask;
//            },
//            OnForbidden = c =>
//            {
//                return Task.CompletedTask;
//            },
//            OnAuthenticationFailed = c =>
//            {
//                // do some logging or whatever...
//                return Task.CompletedTask;
//            }
//        };
//    });


var app = builder.Build();

app.UseHeaderToAuth();

// Configure the HTTP request pipeline.

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();

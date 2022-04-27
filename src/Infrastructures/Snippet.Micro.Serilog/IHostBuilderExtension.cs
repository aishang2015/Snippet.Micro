using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Sinks.Elasticsearch;

namespace Snippet.Micro.Serilog
{
    public static class IHostBuilderExtension
    {
        /// <summary>
        /// 写es日志
        /// </summary>
        public static WebApplicationBuilder AddElasticsearchLog(this WebApplicationBuilder builder,
            bool preserveStaticLogger = false, bool writeToProviders = false)
        {
            var esHost = builder.Configuration["Authority"];
            esHost = string.IsNullOrEmpty(esHost) ? "http://localhost:9200" : esHost;
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(esHost))
                    {
                        MinimumLogEventLevel = LogEventLevel.Verbose,
                    });
            }, preserveStaticLogger, writeToProviders);
            return builder;
        }


        /// <summary>
        /// 写文件日志
        /// </summary>
        public static WebApplicationBuilder AddFileLog(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/all", "log-all-.txt");
                string errorLogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/error", "log-error-.txt");
                string serilogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/serilog", "log-serilog-.txt");
                string logFormat = @"{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3} {SourceContext:l}] {Message:lj}{NewLine}{Exception}";
                
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Logger(config =>
                    {
                        config.WriteTo.File(logPath,
                            restrictedToMinimumLevel: LogEventLevel.Debug,
                            outputTemplate: logFormat,
                            rollingInterval: RollingInterval.Day,
                            rollOnFileSizeLimit: true,
                            shared: true,
                            fileSizeLimitBytes: 10_000_000,
                            retainedFileCountLimit: 30);
                    })
                   .WriteTo.Logger(config =>
                   {
                       config.WriteTo.File(errorLogPath,
                           outputTemplate: logFormat,
                           restrictedToMinimumLevel: LogEventLevel.Error,
                           rollingInterval: RollingInterval.Day,
                           rollOnFileSizeLimit: true,
                           shared: true,
                           fileSizeLimitBytes: 10_000_000,
                           retainedFileCountLimit: 30);
                   })
                   .WriteTo.Logger(config =>
                   {
                       config.WriteTo.File(serilogPath,
                           outputTemplate: logFormat,
                           restrictedToMinimumLevel: LogEventLevel.Warning,
                           rollingInterval: RollingInterval.Day,
                           rollOnFileSizeLimit: true,
                           shared: true,
                           fileSizeLimitBytes: 10_000_000,
                           retainedFileCountLimit: 30);
                       config.Filter.ByIncludingOnly(Matching.FromSource("Serilog.AspNetCore"));
                   });
            });
            return builder;
        }
    }
}

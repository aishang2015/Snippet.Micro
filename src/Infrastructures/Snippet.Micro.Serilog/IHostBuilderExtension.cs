using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace Snippet.Micro.Serilog
{
    public static class IHostBuilderExtension
    {
        public static IHostBuilder AddElasticsearchLog(this IHostBuilder host,
            string esHost = "http://localhost:9200",
            bool preserveStaticLogger = false, bool writeToProviders = false)
        {
            host.UseSerilog((context, services, configuration) =>
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
            return host;
        }
    }
}

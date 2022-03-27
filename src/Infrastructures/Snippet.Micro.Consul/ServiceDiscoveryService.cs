using Consul;

namespace Snippet.Micro.Consul
{
    public interface IServiceDiscoveryService
    {
        Task<List<AgentService>> GetServicesAsync();

        Task<List<AgentService>> GetServicesAsync(string serviceName);
    }

    public class ServiceDiscoveryService : IServiceDiscoveryService
    {
        private readonly IConsulClient _consulClient;

        public ServiceDiscoveryService(IConsulClient consulClient)
        {
            _consulClient = consulClient;
        }

        public async Task<List<AgentService>> GetServicesAsync()
        {
            var result = await _consulClient.Agent.Services();
            return result.Response.Select(x => x.Value).ToList();
        }

        public async Task<List<AgentService>> GetServicesAsync(string serviceName)
        {
            var result = await _consulClient.Health.Service(serviceName, "", true);
            return result.Response.Select(x => x.Service).ToList();
        }
    }
}

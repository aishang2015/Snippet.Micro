using Consul;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Snippet.Micro.Consul
{
    internal class ConsulRegisterService : IHostedService
    {
        private readonly IConsulClient _client;

        private AgentServiceRegistration _registration;

        private readonly ConsulOption _consulOption;

        public ConsulRegisterService(IConsulClient client, IOptions<ConsulOption> consulOptions)
        {
            _client = client;
            _consulOption = consulOptions.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                HTTP = $"http://{_consulOption.ServiceIp}:{_consulOption.ServicePort}/api/health",//健康检查地址
                Timeout = TimeSpan.FromSeconds(5)
            };

            // Register service with consul
            _registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = Guid.NewGuid().ToString(),
                Name = _consulOption.ServiceName,
                Address = _consulOption.ServiceIp,
                Port = _consulOption.ServicePort,
                Tags = new[] { $"urlprefix-/{_consulOption.ServiceName}" }//添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
            };

            await _client.Agent.ServiceRegister(_registration);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // 服务停止时取消注册
            await _client.Agent.ServiceDeregister(_registration.ID);
        }
    }
}

namespace Snippet.Micro.Consul
{
    public class ConsulOption
    {
        // IP = NetworkHelper.LocalIPAddress,
        // Port = Convert.ToInt32(Configuration["Service:Port"]),
        // ServiceName = Configuration["Service:Name"],
        // ConsulIP = Configuration["Consul:IP"],
        // ConsulPort = Convert.ToInt32(Configuration["Consul:Port"])
        public string ServiceIp { get; set; } = NetworkHelper.LocalIPAddress;
        public int ServicePort { get; set; }
        public string ServiceName { get; set; }

        public string ConsulIp { get; set; } = "127.0.0.1";
        public int? ConsulPort { get; set; } = 8500;
    }
}

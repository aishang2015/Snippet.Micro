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

        public string ConsulIp { get; set; }
        public int ConsulPort { get; set; }
    }
}

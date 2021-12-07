using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippet.Micro.MassTransit
{
    public class MassTransitOption
    {
        public string Host { get; set; }

        public ushort Port { get; set; } = 5672;

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}

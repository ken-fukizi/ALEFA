using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Configuration
{
    public class ServiceBusReceiverEndpointConfiguration
    {
        public string QueueName { get; set; }
        public int RateLimitVolume { get; set; }
        public int RateLimitIntervalInSeconds { get; set; }
        public int ConcurrentConsumerMessageLimit { get; set; }
        public ServiceBusCircuitBreakerConfig CircuitBreakerConfig { get; set; }
        public ushort PrefetchCount { get; set; }
    }
}

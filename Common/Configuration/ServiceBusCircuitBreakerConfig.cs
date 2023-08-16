using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Configuration
{
    public class ServiceBusCircuitBreakerConfig
    {
        public int TrackingPeriodInSeconds { get; set; }
        public int TripThreshold { get; set; }
        public int ActiveThreshold { get; set; }
        public int ResetIntervalInSeconds { get; set; }
    }
}

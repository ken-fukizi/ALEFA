using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Configuration
{   
    public class ServiceBusOptions
    {
        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public ushort Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public ServiceBusReceiverEndpointConfiguration AddPatientToLabTestCommandQueue { get; set; }
        
    }

}

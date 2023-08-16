using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Configuration
{   
    public class MassTransitBusOptions
    {
        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public ushort Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public MassTransitReceiverEndPointConfiguration SaveCommunicationRequestEntryCommandQueue { get; set; }
        public MassTransitReceiverEndPointConfiguration AddLeadMediaToVicidialCommandQueue { get; set; }
        public MassTransitReceiverEndPointConfiguration SaveMediaCallbackTelephoneNumberForPersonToOmegaCommandQueue { get; set; }
        public MassTransitReceiverEndPointConfiguration SaveCommunicationRequestEntryWithCreditScoringInformationCommandQueue { get; set; }
        public MassTransitReceiverEndPointConfiguration SaveCommunicationRequestEntryLegacyCommandQueue { get; set; }

        public MassTransitReceiverEndPointConfiguration AddActionExecutionResultCommandQueue { get; set; }
        public MassTransitReceiverEndPointConfiguration AddViciDialActionExecutionResultCommandQueue { get; set; }
        public MassTransitReceiverEndPointConfiguration RegisterConsentTransactionCommandQueue { get; set; }
    }

}

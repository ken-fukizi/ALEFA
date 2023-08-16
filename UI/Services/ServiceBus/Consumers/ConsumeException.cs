using MassTransit;

namespace UI.Services.ServiceBus.Consumers
{
    public class ConsumeException<T> : Exception where T : class
    {
        public ConsumeException(Exception innerException, string message, string commsRequestIdentifier, ConsumeContext<T> context) :
            base(message: message, innerException: innerException)
        {
            CommsRequestIdentifier = commsRequestIdentifier;
            ConsumerContextAttributes = new LoggableConsumerContextAttributes<T>(context);
        }

        public string CommsRequestIdentifier { get; private set; }
        public LoggableConsumerContextAttributes<T> ConsumerContextAttributes { get; private set; }
    }

}

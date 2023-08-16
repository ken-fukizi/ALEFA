using MassTransit;

namespace UI.Services.ServiceBus.Consumers
{
    public class LoggableConsumerContextAttributes<T> where T : class
    {
        public LoggableConsumerContextAttributes(ConsumeContext<T> context)
        {
            Message = context.Message;
            SentTime = context.SentTime;
            InitiatorId = context.InitiatorId;
            ConversationId = context.ConversationId;
            MessageId = context.MessageId;
            RetryCount = context.GetRetryCount();
        }

        public T Message { get; private set; }
        public DateTime? SentTime { get; set; }
        public Guid? InitiatorId { get; set; }
        public Guid? ConversationId { get; private set; }
        public Guid? MessageId { get; private set; }
        public int RetryCount { get; private set; }
    }
}

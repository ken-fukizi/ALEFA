using MassTransit;

namespace UI.Services.ServiceBus.Observers
{
    public class SendObserver : ISendObserver
    {
        private readonly ILogger<SendObserver> _logger;

        public SendObserver(ILogger<SendObserver> logger)
        {
            _logger = logger;
        }
        public Task PreSend<T>(SendContext<T> context)
            where T : class
        {
            // called just before a messageReceivedEvent is sent, all the headers should be setup and everything
            _logger.LogTrace($"PRESEND :: {context.ConversationId} /// {context.MessageId} /// {context.Message} ");
            return Task.CompletedTask;
        }

        public Task PostSend<T>(SendContext<T> context)
            where T : class
        {
            // called just after a messageReceivedEvent it sent to the transport and acknowledged (RabbitMQ)
            _logger.LogTrace($"POSTSEND :: {context.ConversationId} /// {context.MessageId} /// {context.Message}");
            return Task.CompletedTask;
        }

        public Task SendFault<T>(SendContext<T> context, Exception exception)
            where T : class
        {
            // called if an exception occurred sending the messageReceivedEvent
            _logger.LogError(exception, $"SENDFAULT :: {context.ConversationId} /// {context.MessageId} /// {context.Message}");
            return Task.CompletedTask;
        }
    }
}

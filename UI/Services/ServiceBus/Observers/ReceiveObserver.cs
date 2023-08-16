using MassTransit;
using Newtonsoft.Json;

namespace UI.Services.ServiceBus.Observers
{
    public class ReceiveObserver : IReceiveObserver
    {
        private readonly ILogger<ReceiveObserver> _logger;

        public ReceiveObserver(ILogger<ReceiveObserver> logger)
        {
            _logger = logger;
        }


        public Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception) where T : class
        {
            _logger.LogTrace($"CONSUMER FAULT: {consumerType} /// {context.ConversationId} /// {context.MessageId} /// {duration.ToString()} /// {exception.Message}");
            return Task.CompletedTask;
        }

        public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
        {
            _logger.LogTrace($"POST CONSUME: {consumerType} /// {context.ConversationId} /// {context.MessageId} {duration.ToString()} /// {JsonConvert.SerializeObject(context.Message)}");
            return Task.CompletedTask;
        }

        public Task PreReceive(ReceiveContext context)
        {
            _logger.LogTrace($"PRE RECEIVE:");
            return Task.CompletedTask;
        }

        public Task PostReceive(ReceiveContext context)
        {
            _logger.LogTrace($"POST RECEIVE:");
            return Task.CompletedTask;
        }

        public Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            _logger.LogTrace($"RECEIVE FAULT:  {exception.Message} ");
            return Task.CompletedTask;
        }
    }
}

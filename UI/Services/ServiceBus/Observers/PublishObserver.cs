using MassTransit;

namespace UI.Services.ServiceBus.Observers
{
    public class PublishObserver : IPublishObserver
    {
        private readonly ILogger<PublishObserver> _loggerService;

        public PublishObserver(ILogger<PublishObserver> loggerService)
        {
            _loggerService = loggerService;
        }
        public Task PostPublish<T>(PublishContext<T> context) where T : class
        {
            _loggerService.LogTrace($"POST PUBLISH: {context.ConversationId} /// {context.MessageId}");
            return Task.CompletedTask;
        }

        public Task PrePublish<T>(PublishContext<T> context) where T : class
        {

            _loggerService.LogTrace($"PRE PUBLISH: {context.ConversationId} /// {context.MessageId}");
            return Task.CompletedTask;
        }

        public Task PublishFault<T>(PublishContext<T> context, Exception exception) where T : class
        {
            _loggerService.LogTrace($"PUBLISH FAULT: {context.ConversationId} /// {context.MessageId} /// {exception.Message}");

            return Task.CompletedTask;
        }
    }
}

using MassTransit;

namespace UI.Services.ServiceBus.Observers
{
    public class BusObserver : IBusObserver
    {
        private readonly ILogger<BusObserver> _logger;

        public BusObserver(ILogger<BusObserver> logger)
        {
            _logger = logger;
        }
        public Task PostCreate(IBus bus)
        {
            // called after the bus has been created, but before it has been started.
            _logger.LogInformation($"{nameof(PostCreate)}");

            return Task.CompletedTask;
        }

        public Task CreateFaulted(Exception exception)
        {
            // called if the bus creation fails for some reason
            _logger.LogInformation($"{nameof(CreateFaulted)} ::: {exception.Message}");

            return Task.CompletedTask;
        }

        public Task PreStart(IBus bus)
        {
            // called just before the bus is started
            _logger.LogTrace($"{nameof(PreStart)}");

            return Task.CompletedTask;
        }

        public Task PostStart(IBus bus, Task<BusReady> busReady)
        {
            // called once the bus has been started successfully. The task can be used to wait for
            // all of the receive endpoints to be ready.
            _logger.LogTrace($"{nameof(PostStart)}");
            return Task.CompletedTask;
        }

        public Task StartFaulted(IBus bus, Exception exception)
        {
            // called if the bus fails to start for some reason (dead battery, no fuel, etc.)
            _logger.LogError($"{nameof(StartFaulted)} ::: {exception.Message}");
            return Task.CompletedTask;
        }

        public Task PreStop(IBus bus)
        {
            // called just before the bus is stopped
            _logger.LogInformation($"{nameof(PreStop)}");

            return Task.CompletedTask;
        }

        public Task PostStop(IBus bus)
        {
            // called after the bus has been stopped
            _logger.LogInformation($"{nameof(PostStop)}");
            return Task.CompletedTask;
        }

        public Task StopFaulted(IBus bus, Exception exception)
        {
            // called if the bus fails to stop (no brakes)
            _logger.LogInformation($"{nameof(StartFaulted)} ::: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}

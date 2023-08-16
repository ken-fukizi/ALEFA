using MassTransit;

namespace UI.Services.ServiceBus
{
    public class ApplicationBus
    {
        private readonly ILogger<ApplicationBus> _logger;
        private readonly IBus _bus;
        public ApplicationBus(ILogger<ApplicationBus> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }
    }
}

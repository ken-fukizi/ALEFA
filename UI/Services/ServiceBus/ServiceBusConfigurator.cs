using GreenPipes;
using MassTransit.RabbitMqTransport;
using MassTransit;
using SharedKernel.Exceptions;
using Newtonsoft.Json;
using Common.Configuration;
using UI.Services.ServiceBus.Commands;
using UI.Services.ServiceBus.Consumers;

namespace UI.Services.ServiceBus
{
    public static class ServiceBusConfigurator
    {
        public static IBusControl ConfigureBus(IRegistrationContext<IServiceProvider> registration, ServiceBusOptions serviceBusOptions, IPublishObserver publishObserver, IReceiveObserver receiveObserver, IBusObserver busObserver, ISendObserver sendObserver)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(
                    host: serviceBusOptions.Host,
                    port: serviceBusOptions.Port,
                    virtualHost: serviceBusOptions.VirtualHost,
                    configure: h =>
                    {
                        h.Username(serviceBusOptions.UserName);
                        h.Password(serviceBusOptions.Password);
                    }
                );
                cfg.Durable = true;
                cfg.AutoDelete = true;

                cfg.ConfigureJsonSerializer(jsonSer =>
                {
                    jsonSer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    return jsonSer;
                });

                cfg.BusObserver(busObserver);

                #region StandardEndPoints
                //Can configure as many endpoints as needed here
                ConfigureStandardEndPoint<AddPatientToLabTestCommandConsumer, AddPatientToLabTestCommand>(
                    registration,
                    cfg,
                    host,
                    serviceBusOptions.AddPatientToLabTestCommandQueue,
                    new Type[] { typeof(PermanentException) });

                #endregion



            });


            bus.ConnectSendObserver(sendObserver);
            bus.ConnectReceiveObserver(receiveObserver);
            bus.ConnectPublishObserver(publishObserver);

            return bus;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TConsumer"></typeparam>
        /// <typeparam name="TMessageType"></typeparam>
        /// <param name="provider"></param>
        /// <param name="cfg"></param>
        /// <param name="host"></param>
        /// <param name="serviceBusReceiverEndPointConfiguration"></param>
        /// <param name="ignoreExceptions">An array of exception types that will be ignore for retries. IE if this exception occurs the messageReceivedEvent will be moved directly to an error queue</param>
        private static void ConfigureStandardEndPoint<TConsumer, TMessageType>(
            IRegistrationContext<IServiceProvider> registration,
            IRabbitMqBusFactoryConfigurator cfg,
            IRabbitMqHost host,
            ServiceBusReceiverEndpointConfiguration serviceBusReceiverEndPointConfiguration,
            Type[] ignoreExceptions) where TMessageType : class where TConsumer : class, IConsumer<TMessageType>
        {
            cfg.ReceiveEndpoint(serviceBusReceiverEndPointConfiguration.QueueName, e =>
            {
                e.AutoDelete = false;
                e.PrefetchCount = serviceBusReceiverEndPointConfiguration.PrefetchCount;

                e.UseMessageRetry(retry =>
                {
                    
                    retry.Interval(3, TimeSpan.FromHours(6));

                    retry.Handle<Exception>();
                    foreach (var type in ignoreExceptions)
                    {
                        retry.Ignore(type);
                    }
                });

                e.UseCircuitBreaker(cb =>
                {
                    cb.TrackingPeriod = TimeSpan.FromSeconds(serviceBusReceiverEndPointConfiguration.CircuitBreakerConfig.TrackingPeriodInSeconds);
                    cb.TripThreshold = serviceBusReceiverEndPointConfiguration.CircuitBreakerConfig.TripThreshold;
                    cb.ActiveThreshold = serviceBusReceiverEndPointConfiguration.CircuitBreakerConfig.ActiveThreshold;
                    cb.ResetInterval = TimeSpan.FromSeconds(serviceBusReceiverEndPointConfiguration.CircuitBreakerConfig.ResetIntervalInSeconds);
                    cb.Handle<Exception>();
                    foreach (var type in ignoreExceptions)
                    {
                        cb.Ignore(type);
                    }
                });

                e.UseRateLimit(
                    rateLimit: serviceBusReceiverEndPointConfiguration.RateLimitVolume,
                    interval: TimeSpan.FromSeconds(serviceBusReceiverEndPointConfiguration.RateLimitIntervalInSeconds));

                e.ConfigureConsumer<TConsumer>(registration, consumerCfg =>
                {
                    consumerCfg.UseConcurrentMessageLimit(serviceBusReceiverEndPointConfiguration.ConcurrentConsumerMessageLimit);
                });
                EndpointConvention.Map<TMessageType>(e.InputAddress);
            });

        }
    }

}

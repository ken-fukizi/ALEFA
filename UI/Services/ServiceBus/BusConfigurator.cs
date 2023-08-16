using GreenPipes;
using MassTransit.RabbitMqTransport;
using MassTransit;
using SharedKernel.Exceptions;
using Newtonsoft.Json;
using Common.Configuration;
using UI.Services.ServiceBus.Commands;

namespace UI.Services.ServiceBus
{
    public static class BusConfigurator
    {
        public static IBusControl ConfigureBus(IRegistrationContext<IServiceProvider> registration, MassTransitBusOptions massTransitBusOptions, IPublishObserver publishObserver, IReceiveObserver receiveObserver, IBusObserver busObserver, ISendObserver sendObserver)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(
                    host: massTransitBusOptions.Host,
                    port: massTransitBusOptions.Port,
                    virtualHost: massTransitBusOptions.VirtualHost,
                    configure: h =>
                    {
                        h.Username(massTransitBusOptions.UserName);
                        h.Password(massTransitBusOptions.Password);
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

                
                ConfigureStandardEndPoint<TestSaveCommandConsumer, TestSaveCommand>(
                    registration,
                    cfg,
                    host,
                    massTransitBusOptions.SaveCommunicationRequestEntryCommandQueue,
                    new Type[] { typeof(PermanentException) });                
                
                ConfigureStandardEndPoint<AddPatientToLabTestCommandConsumer, AddPatientToLabTestCommand>(
                    registration,
                    cfg,
                    host,
                    massTransitBusOptions.AddPatientToLabTestCommandQueue,
                    new Type[] { typeof(PermanentException) });
                
            }
            );


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
        /// <param name="massTransitReceiverEndPointConfiguration"></param>
        /// <param name="ignoreExceptions">An array of exception types that will be ignore for retries. IE if this exception occurs the messageReceivedEvent will be moved directly to an error queue</param>
        private static void ConfigureStandardEndPoint<TConsumer, TMessageType>(
            IRegistrationContext<IServiceProvider> registration,
            IRabbitMqBusFactoryConfigurator cfg,
            IRabbitMqHost host,
            MassTransitReceiverEndPointConfiguration massTransitReceiverEndPointConfiguration,
            Type[] ignoreExceptions) where TMessageType : class where TConsumer : class, IConsumer<TMessageType>
        {
            cfg.ReceiveEndpoint(massTransitReceiverEndPointConfiguration.QueueName, e =>
            {
                e.AutoDelete = false;
                e.PrefetchCount = massTransitReceiverEndPointConfiguration.PrefetchCount;

                e.UseMessageRetry(retry =>
                {
                    //retry.Immediate(int.MaxValue);
                    //ToDo: KF - this retry policy should be set specifically for the AddLeadMediaToVicidialCommand queue. All the other queus should use the immediate retry policy
                    retry.Interval(3, TimeSpan.FromHours(6));

                    retry.Handle<Exception>();
                    foreach (var type in ignoreExceptions)
                    {
                        retry.Ignore(type);
                    }
                });

                e.UseCircuitBreaker(cb =>
                {
                    cb.TrackingPeriod = TimeSpan.FromSeconds(massTransitReceiverEndPointConfiguration.CircuitBreakerConfig.TrackingPeriodInSeconds);
                    cb.TripThreshold = massTransitReceiverEndPointConfiguration.CircuitBreakerConfig.TripThreshold;
                    cb.ActiveThreshold = massTransitReceiverEndPointConfiguration.CircuitBreakerConfig.ActiveThreshold;
                    cb.ResetInterval = TimeSpan.FromSeconds(massTransitReceiverEndPointConfiguration.CircuitBreakerConfig.ResetIntervalInSeconds);
                    cb.Handle<Exception>();
                    foreach (var type in ignoreExceptions)
                    {
                        cb.Ignore(type);
                    }
                });

                e.UseRateLimit(
                    rateLimit: massTransitReceiverEndPointConfiguration.RateLimitVolume,
                    interval: TimeSpan.FromSeconds(massTransitReceiverEndPointConfiguration.RateLimitIntervalInSeconds));

                e.ConfigureConsumer<TConsumer>(registration, consumerCfg =>
                {
                    consumerCfg.UseConcurrentMessageLimit(massTransitReceiverEndPointConfiguration.ConcurrentConsumerMessageLimit);
                });
                EndpointConvention.Map<TMessageType>(e.InputAddress);
            });

        }
    }

}

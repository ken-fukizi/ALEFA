using MassTransit;
using System;
using UI.Services.ServiceBus.Commands;

namespace UI.Services.ServiceBus.Consumers
{
    public class AddPatientToLabTestCommandConsumer : IConsumer<AddPatientToLabTestCommand>
    {
        public async Task Consume(ConsumeContext<AddPatientToLabTestCommand> context)
        {
            //Publish or send to specific queues

            await context.Send<AddPatientToLabTestCommand>(
                        new
                        {
                            CommandId = context.CorrelationId,
                            CommandDateTime = DateTime.UtcNow,
                            PatientRequestId = 1,
                            PatientGuid = Guid.NewGuid()
                            
                        }
                    );

            
            
        }
    }
}

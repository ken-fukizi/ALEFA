namespace UI.Services.ServiceBus.Commands
{
    public interface AddPatientToLabTestCommand
    {
        Guid CommandId { get; set; }
        DateTime CommandDateTime { get; set; }
        int PatientRequestId { get; set; }
        Guid PatientGuid { get; set; }
    }
}

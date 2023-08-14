namespace UI.Domain.Events
{
    public interface IPatientReceivedEvent
    {
        public Guid EventId { get; set; }
        public Guid PatientGuid { get; set; }
        public long PatientIdNumber { get; set;}
    }
}

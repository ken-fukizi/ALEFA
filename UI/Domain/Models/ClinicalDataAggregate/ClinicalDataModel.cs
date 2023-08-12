using SharedKernel;

namespace UI.Domain.Models.ClinicalDataAggregate
{
    public class ClinicalDataModel : AggregateRoot
    {
        public static class Factory
        {
            public static ClinicalDataModel Create(Guid patientGuid, double temperature, double hgCount, DateTime? lastVisitDateTime, string lastPrescriptions) 
            {
                var newClinicalDataModel = new ClinicalDataModel
                    (
                        patientGuid: patientGuid, 
                        temperature: temperature, 
                        hgCount: hgCount, 
                        lastVisitDateTime: lastVisitDateTime, 
                        lastPrescriptions: lastPrescriptions    
                    );
                return newClinicalDataModel; 
            }
        }
        public ClinicalDataModel(Guid patientGuid, double temperature, double hgCount , DateTime? lastVisitDateTime, string lastPrescriptions)
        {
            PatientGuid = patientGuid;
            Temperature = temperature;
            HgCount = hgCount;
            LastVisitDateTime = lastVisitDateTime;
            LastPrescriptions = lastPrescriptions;
                      
        }
        public Guid PatientGuid { get; private set; }
        public double Temperature { get; private set; }
        public double HgCount { get; private set; }
        public DateTime? LastVisitDateTime { get; private set; }
        public string LastPrescriptions { get; private set; } = string.Empty;
    }
}

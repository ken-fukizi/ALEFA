using SharedKernel;

namespace UI.Domain.Models.DemographicsAggregate
{
    public class DemographicsModel : AggregateRoot
    {
        public static class Factory
        {
            public static DemographicsModel Create(Guid patientGuid, string currentTown, string previousTowns, string occupation)
            {
                var newDemographicsModel = 
                    new DemographicsModel
                    (
                        patientGuid: patientGuid, 
                        currentTown: currentTown, 
                        previousTowns: previousTowns, 
                        occupation: occupation
                    );
                return newDemographicsModel;
            }
        }
        public DemographicsModel(Guid patientGuid, string currentTown, string previousTowns, string occupation) 
        {
            PatientGuid = patientGuid;
            CurrentTown = currentTown;
            PreviousTowns = previousTowns;
            Occupation = occupation;
        }
        public Guid PatientGuid { get; private set; }
        public string CurrentTown { get; private set; }
        public string PreviousTowns { get; private set; }
        public string Occupation { get; private set; }
    }
}

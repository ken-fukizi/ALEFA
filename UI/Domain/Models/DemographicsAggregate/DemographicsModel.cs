using SharedKernel;

namespace UI.Domain.Models.DemographicsAggregate
{
    public class DemographicsModel : AggregateRoot
    {
        public static class Factory
        {
            public static DemographicsModel Create(Guid patientGuid, string currentTown, string previousTowns, string occupation, string country, string region)
            {
                var newDemographicsModel = 
                    new DemographicsModel
                    (
                        patientGuid: patientGuid, 
                        currentTown: currentTown, 
                        previousTowns: previousTowns, 
                        occupation: occupation,
                        country: country, 
                        region: region
                    );
                return newDemographicsModel;
            }
        }
        public DemographicsModel(Guid patientGuid, string currentTown, string previousTowns, string occupation, string country, string region) 
        {
            PatientGuid = patientGuid;
            CurrentTown = currentTown;
            PreviousTowns = previousTowns;
            Occupation = occupation;
            Country = country;
            Region = region;
        }
        public Guid PatientGuid { get; private set; }
        public string CurrentTown { get; private set; }
        public string PreviousTowns { get; private set; }
        public string Occupation { get; private set; }
        public string Country { get; private set; }
        public string Region { get; private set; }
    }
}

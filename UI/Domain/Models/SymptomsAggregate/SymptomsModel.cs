namespace UI.Domain.Models.SymptomsAggregate
{
    public class SymptomsModel
    {
        public static class Factory
        {
            public static SymptomsModel Create(Guid patientGuid, IEnumerable<string> options)
            {
                var newSymptomsModel = new SymptomsModel(patientGuid: patientGuid, options:options);
                return newSymptomsModel; 
            }
        }
        public SymptomsModel(Guid patientGuid, IEnumerable<string> options)
        {
            PatientGuid = patientGuid;
            Options = options;
        }
        public Guid PatientGuid { get; set; }
        public IEnumerable<string> Options { get; set; } 
    }
}

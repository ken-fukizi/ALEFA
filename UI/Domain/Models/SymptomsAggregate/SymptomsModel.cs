﻿using SharedKernel;

namespace UI.Domain.Models.SymptomsAggregate
{
    public class SymptomsModel : AggregateRoot
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
        public Guid PatientGuid { get; private set; }
        public IEnumerable<string> Options { get; private set; } 
    }
}

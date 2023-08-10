using SharedKernel;
using UI.Areas.PatientCaseReview.Pages.ViewModels;
using UI.Domain.Models.Enumerations;
using UI.Domain.Models.PatientAggregate;

namespace UI.Services
{
    public class PatientDetailsService : IPatientDetailsService
    {
        public void SavePatientDetails(PatientViewModel patientDetails)
        {
            var patient = PatientModel.Factory.Create
                (
                    firstName: patientDetails.FirstName, 
                    lastName:patientDetails.LastName, 
                    email: patientDetails.Email, 
                    phoneNumber: patientDetails.PhoneNumber, 
                    //gender: Enumeration.FromDisplayName<EnumGender>(patientDetails.Gender.ToString()),
                    gender: patientDetails.Gender,
                    age: patientDetails.Age,
                    weight:patientDetails.Weight,
                    height: patientDetails.Height
                );
            
        }
    }
}

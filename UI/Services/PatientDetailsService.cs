using SharedKernel;
using UI.Areas.PatientCaseReview.Pages.ViewModels;
using UI.Data;
using UI.Domain.Models.Enumerations;
using UI.Domain.Models.PatientAggregate;

namespace UI.Services
{
    public class PatientDetailsService : IPatientDetailsService
    {
        private readonly ApplicationDbContext _dbContext;
        public PatientDetailsService(ApplicationDbContext context)
        {
            _dbContext = context;
        }
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
            //Bypassing the usage of the repository pattern for now.

            //ToDo: Check if it exists first
            _dbContext.Patients.Add( patient );
            _dbContext.SaveChanges();
            
        }
    }
}

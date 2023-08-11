using UI.Areas.PatientCaseReview.Pages.ViewModels;

namespace UI.Services
{
    public interface IPatientDetailsService
    {
        Guid SavePatientDetails(PatientViewModel patientDetails);
        void SaveDemographics(DemographicsViewModel demographics);
    }
}

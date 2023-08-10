using UI.Areas.PatientCaseReview.Pages.ViewModels;

namespace UI.Services
{
    public interface IPatientDetailsService
    {
        void SavePatientDetails(PatientViewModel patient);
    }
}

using UI.Areas.PatientCaseReview.Pages;
using UI.Areas.PatientCaseReview.Pages.ViewModels;

namespace UI.Services
{
    public interface IPatientDetailsService
    {
        Guid SavePatientDetails(PatientDetailsViewModel patientDetails);
        void SaveDemographics(DemographicsViewModel demographics);

        void SavePatientSymptoms(SymptomsViewModel symptoms);
        void SavePatientClinicalData(ClinicalDataViewModel clinicalData);
    }
}

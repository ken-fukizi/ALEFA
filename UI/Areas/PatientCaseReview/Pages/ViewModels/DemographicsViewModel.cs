namespace UI.Areas.PatientCaseReview.Pages.ViewModels
{
    public class DemographicsViewModel
    {
        public Guid PatientGuid { get; set; }
        public string CurrentTown { get; set; }
        public string PreviousTowns { get; set; }
        public string Occupation { get; set;}
    }
}

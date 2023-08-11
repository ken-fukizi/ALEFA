namespace UI.Areas.PatientCaseReview.Pages.ViewModels
{
    public class ClinicalDataViewModel
    {
        public Guid PatientGuid { get; set; }
        public int Temperature { get; set; }
        public int HgCount { get; set;} 
        public DateTime? LastVisitDateTime { get; set; } 
        public string[]? LastPrescriptions { get; set;}
    }
}

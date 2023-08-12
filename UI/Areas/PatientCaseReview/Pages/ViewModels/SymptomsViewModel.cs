namespace UI.Areas.PatientCaseReview.Pages.ViewModels
{
    public class SymptomsViewModel
    {
        public Guid PatientGuid { get; set; }        
        public IEnumerable<string> Options { get; set; } = new HashSet<string>() { };
    }
}

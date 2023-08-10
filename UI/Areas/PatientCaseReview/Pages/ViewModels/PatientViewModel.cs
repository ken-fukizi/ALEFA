using UI.Areas.PatientCaseReview.Pages.ViewModels.Enums;

namespace UI.Areas.PatientCaseReview.Pages.ViewModels
{
    public class PatientViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public Int16 Age { get; set; }
        public Int16 Weight { get; set; }
        public Int16 Height { get; set; }
    }
}

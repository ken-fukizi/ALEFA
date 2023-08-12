using UI.Areas.PatientCaseReview.Pages.ViewModels.Enums;

namespace UI.Areas.PatientCaseReview.Pages.ViewModels
{
    public class PatientDetailsViewModel
    {
        public string Identifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
    }
}

using System.ComponentModel;

namespace UI.Areas.PatientCaseReview.Pages.ViewModels.Enums
{
    public enum Gender
    {
        Male = 1, Female = 2
    }


    public enum AccountType
    {
        [Description("Current Account")]
        Current = 1,
        [Description("Savings Account")]
        Savings = 2,
        [Description("Transmission Account")]
        Transmission = 3,
        [Description("Bonds Account")]
        Bonds = 4
    }
}

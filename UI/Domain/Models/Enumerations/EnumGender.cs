using SharedKernel;

namespace UI.Domain.Models.Enumerations
{
    public class EnumGender : Enumeration
    {
        public static readonly EnumGender Male = new EnumGender(id: 1, displayName: "Male");
        public static readonly EnumGender Female = new EnumGender(id: 1, displayName: "Female");
        private EnumGender() { }
        private EnumGender(int id, string displayName) : base(id, displayName) { }
        
    }
}

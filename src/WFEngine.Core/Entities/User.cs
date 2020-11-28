using Dapper.Contrib.Extensions;
using WFEngine.Core.Enums;

namespace WFEngine.Core.Entities
{
    [Table("user")]
    public class User : BaseEntity
    {
        public enumLanguage LanguageId { get; set; }
        public enumLoginType LoginTypeId { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool EmailVerificated { get; set; }

        public User()
        {
            LanguageId= enumLanguage.EN;
            LoginTypeId = enumLoginType.Default;
            TwoFactorEnabled = false;
            EmailVerificated = false;
        }
    }
}

using Dapper.Contrib.Extensions;

namespace WFEngine.Core.Entities
{
    [Table("logintype")]
    public class LoginType : BaseEntity
    {
        public string Name { get; set; }
        public string GlobalName { get; set; }
    }
}

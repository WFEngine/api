using Dapper.Contrib.Extensions;

namespace WFEngine.Core.Entities
{
    [Table("organization")]
    public class Organization : BaseEntity
    {
        public string Name { get; set; }
    }
}

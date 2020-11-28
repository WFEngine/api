using Dapper.Contrib.Extensions;

namespace WFEngine.Core.Entities
{
    [Table("language")]
    public class Language : BaseEntity
    {
        public string Name { get; set; }
        public string GlobalName { get; set; }
    }
}

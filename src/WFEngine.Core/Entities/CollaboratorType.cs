using Dapper.Contrib.Extensions;

namespace WFEngine.Core.Entities
{
    [Table("collaboratortype")]
    public class CollaboratorType : BaseEntity
    {
        public string Name { get; set; }
        public string GlobalName { get; set; }
    }
}

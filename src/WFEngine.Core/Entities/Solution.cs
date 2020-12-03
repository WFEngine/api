using Dapper.Contrib.Extensions;

namespace WFEngine.Core.Entities
{
    [Table("solution")]
    public class Solution : BaseEntity
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Write(false)]
        public string OrganizationName { get; set; }

        [Write(false)]
        public int CollaboratorTypeId { get; set; }
    }
}

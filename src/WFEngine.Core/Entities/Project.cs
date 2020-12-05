using Dapper.Contrib.Extensions;
using System;

namespace WFEngine.Core.Entities
{
    [Table("project")]
    public class Project : BaseEntity
    {
        public string UniqueKey { get; set; }
        public int OrganizationId { get; set; }
        public int SolutionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Project()
        {
            UniqueKey = Guid.NewGuid().ToString();
        }
    }
}

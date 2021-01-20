using Dapper.Contrib.Extensions;
using System;

namespace WFEngine.Core.Entities
{
    [Table("solution")]
    public class Solution : BaseEntity
    {
        public string UniqueKey { get; set; }
        public int OrganizationId { get; set; }
        public int PackageVersionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Write(false)]
        public string OrganizationName { get; set; }

        [Write(false)]
        public int CollaboratorTypeId { get; set; }
        [Write(false)]
        public string PackageVersion { get; set; }

        public Solution()
        {
            UniqueKey = Guid.NewGuid().ToString();
        }
    }
}

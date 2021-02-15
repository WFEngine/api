using Dapper.Contrib.Extensions;

namespace WFEngine.Core.Entities
{
    [Table("variabletype")]
    public class VariableType : BaseEntity
    {
        public int PackageVersionId { get; set; }
        public string Type { get; set; }

        [Write(false)]
        public string PackageVersionName { get; set; }
    }
}

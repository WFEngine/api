using Dapper.Contrib.Extensions;

namespace WFEngine.Core.Entities
{
    [Table("packageversion")]
    public class PackageVersion : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Version { get; set; }
    }
}

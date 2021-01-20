using Dapper.Contrib.Extensions;

namespace WFEngine.Core.Entities
{
    [Table("activity")]
    public class Activity : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int ActivityTypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PackageVersionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AssemblyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ActivityName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsMainActivity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsPlatformBased { get; set; }
    }
}

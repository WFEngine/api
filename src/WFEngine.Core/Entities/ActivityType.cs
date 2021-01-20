using Dapper.Contrib.Extensions;

namespace WFEngine.Core.Entities
{
    [Table("activitytype")]
    public class ActivityType : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string GlobalName { get; set; }
    }
}

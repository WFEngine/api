using Dapper.Contrib.Extensions;

namespace WFEngine.Core.Entities
{
    [Table("wfobjecttype")]
    public class WFObjectType : BaseEntity
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

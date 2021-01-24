using Dapper.Contrib.Extensions;

namespace WFEngine.Core.Entities
{
    [Table("wfobject")]
    public class WFObject :BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int WfObjectTypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SolutionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }

        [Write(false)]
        public string WFObjectTypeName { get; set; }
    }
}

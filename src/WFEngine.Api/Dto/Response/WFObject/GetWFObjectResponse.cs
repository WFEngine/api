namespace WFEngine.Api.Dto.Response.WFObject
{
    /// <summary>
    /// 
    /// </summary>
    public class GetWFObjectResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public WFObjectItem WFObject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public sealed class WFObjectItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int WfObjectTypeId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Value { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public GetWFObjectResponse()
        {
            WFObject = new WFObjectItem();
        }
    }
}

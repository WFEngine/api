using System.Collections.Generic;

namespace WFEngine.Api.Dto.Response.VariableType
{
    /// <summary>
    /// 
    /// </summary>
    public class GetVariableTypeResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public List<VariableTypeItem> VariableTypes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public sealed class VariableTypeItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string PackageVersionName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Type { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public GetVariableTypeResponse()
        {
            VariableTypes = new List<VariableTypeItem>();
        }
    }
}

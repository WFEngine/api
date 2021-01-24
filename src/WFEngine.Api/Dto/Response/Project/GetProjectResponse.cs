using System.Collections.Generic;

namespace WFEngine.Api.Dto.Response.Project
{
    /// <summary>
    /// 
    /// </summary>
    public class GetProjectResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public ProjectItem Project{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<WFObject> WFObjects { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public sealed class ProjectItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string UniqueKey { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int ProjectTypeId { get; set; }            
            /// <summary>
            /// 
            /// </summary>
            public int OrganizationId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string OrganizationName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int SolutionId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string SolutionName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Description { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public sealed class WFObject
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }            
            /// <summary>
            /// 
            /// </summary>
            public int WFObjectTypeId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string WFObjectTypeName { get; set; }
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
        public GetProjectResponse()
        {
            WFObjects = new List<WFObject>();
        }
    }
}

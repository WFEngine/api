using System.Collections.Generic;

namespace WFEngine.Api.Dto.Response.Project
{
    /// <summary>
    /// 
    /// </summary>
    public class GetProjectTypesResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public GetProjectTypesResponse()
        {
            ProjectTypes = new List<ProjectType>();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<ProjectType> ProjectTypes{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        public sealed class ProjectType
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }
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
}

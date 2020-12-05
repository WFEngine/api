using System.Collections.Generic;

namespace WFEngine.Api.Dto.Response.Solution
{
    /// <summary>
    /// 
    /// </summary>
    public class SolutionsResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Solution> Solutions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SolutionsResponse()
        {
            Solutions = new List<Solution>();
        }

        /// <summary>
        /// 
        /// </summary>
        public sealed class Solution
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
            public string Description { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string OrganizationName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int CollaboratorTypeId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Project> Projects { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public Solution()
            {
                Projects = new List<Project>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public sealed class Project
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
            public string Description { get; set; }
        }
    }
}

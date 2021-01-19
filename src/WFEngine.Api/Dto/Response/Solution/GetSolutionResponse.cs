using System.Collections.Generic;

namespace WFEngine.Api.Dto.Response.Solution
{
    /// <summary>
    /// 
    /// </summary>
    public class GetSolutionResponse
    {
        /// <summary>
        /// 
        /// </summary>

        public GetSolutionResponse()
        {
            solution = new Solution();
        }

        /// <summary>
        /// 
        /// </summary>
        public Solution solution { get; set; }

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
            public List<Colllaborator> Collaborators { get; set; }    

            /// <summary>
            /// 
            /// </summary>
            public Solution()
            {
                Projects = new List<Project>();
                Collaborators = new List<Colllaborator>();
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
            /// <summary>
            /// 
            /// </summary>
            public string ProjectType { get; set; }
        }


        /// <summary>
        /// 
        /// </summary>
        public sealed class Colllaborator
        {
            /// <summary>
            /// 
            /// </summary>
            public string CollaboratorTypeName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string OrganizationName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string UserName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Email { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Avatar { get; set; }
        }
    }
}

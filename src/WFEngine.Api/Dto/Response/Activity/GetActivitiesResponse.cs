using System.Collections.Generic;

namespace WFEngine.Api.Dto.Response.Activity
{
    /// <summary>
    /// 
    /// </summary>
    public class GetActivitiesResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ActivityTypeItem> Activities { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GetActivitiesResponse()
        {
            Activities = new List<ActivityTypeItem>();
        }

        /// <summary>
        /// 
        /// </summary>
       public sealed class ActivityTypeItem
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
            public List<ActivityItem> Activities  { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public ActivityTypeItem()
            {
                Activities = new List<ActivityItem>();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public sealed class ActivityItem
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
            public string AssemblyName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ActivityName { get; set; }
        }
    }
}

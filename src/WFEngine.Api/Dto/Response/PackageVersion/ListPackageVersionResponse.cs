using System.Collections.Generic;

namespace WFEngine.Api.Dto.Response.PackageVersion
{
    /// <summary>
    /// 
    /// </summary>
    public class ListPackageVersionResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public List<PackageVersion> PackageVersions{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        public sealed class PackageVersion
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Version { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ListPackageVersionResponse()
        {
            PackageVersions = new List<PackageVersion>();
        }
    }
}

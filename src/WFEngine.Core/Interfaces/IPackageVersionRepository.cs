using System.Collections.Generic;
using WFEngine.Core.Entities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Core.Interfaces
{
    public interface IPackageVersionRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDataResult<List<PackageVersion>> GetPackageVersions();
    }
}

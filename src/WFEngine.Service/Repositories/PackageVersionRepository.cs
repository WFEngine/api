using System.Collections.Generic;
using System.Data;
using System.Linq;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Service.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class PackageVersionRepository : BaseRepository, IPackageVersionRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        public PackageVersionRepository(IDbTransaction transaction) 
            : base(transaction)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDataResult<List<PackageVersion>> GetPackageVersions()
        {
            var packageVersions = connection.ExecuteCommand<PackageVersion>("SELECT * FROM packageversion WHERE Status = 1").ToList();
            return new SuccessDataResult<List<PackageVersion>>(packageVersions);
        }
    }
}

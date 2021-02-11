using System.Collections.Generic;
using System.Data;
using System.Linq;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Service.Repositories
{
    public class ActivityRepository : BaseRepository, IActivityRepository
    {
        public ActivityRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public IDataResult<List<Activity>> GetActivities(List<int> activityTypeIds, List<int> packageVersionIds)
        {
            string sql = $"SELECT * FROM activity a WHERE a.ActivityTypeId IN({string.Join(',',activityTypeIds)}) AND a.PackageVersionId IN({string.Join(',',packageVersionIds)}) AND a.Status = 1;";
            var activities = connection.ExecuteCommand<Activity>(sql, activityTypeIds, packageVersionIds)?.ToList();
            return new SuccessDataResult<List<Activity>>(activities);
        }
    }
}

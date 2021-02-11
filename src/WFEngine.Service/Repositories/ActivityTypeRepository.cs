using System.Collections.Generic;
using System.Data;
using System.Linq;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Service.Repositories
{
    public class ActivityTypeRepository : BaseRepository, IActivityTypeRepository
    {
        public ActivityTypeRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public IDataResult<List<ActivityType>> GetActivityTypes()
        {
            string sql = @"SELECT * FROM activitytype WHERE Status = 1";
            var activityTypes = connection.ExecuteCommand<ActivityType>(sql)?.ToList();
            return new SuccessDataResult<List<ActivityType>>(activityTypes);
        }
    }
}

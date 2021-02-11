using System.Collections.Generic;
using WFEngine.Core.Entities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Core.Interfaces
{
    public interface IActivityRepository
    {
        IDataResult<List<Activity>> GetActivities(List<int> activityTypeIds, List<int> packageVersionIds);
    }
}

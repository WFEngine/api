using System.Collections.Generic;
using WFEngine.Core.Entities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Core.Interfaces
{
    public interface IActivityTypeRepository
    {
        IDataResult<List<ActivityType>> GetActivityTypes();
    }
}

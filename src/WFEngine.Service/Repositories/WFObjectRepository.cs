using System.Collections.Generic;
using System.Data;
using System.Linq;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Service.Repositories
{
    public class WFObjectRepository : BaseRepository, IWFObjectRepository
    {
        public WFObjectRepository(IDbTransaction transaction) 
            : base(transaction)
        {
        }

        public IDataResult<List<WFObject>> GetWFObjects(int projectId)
        {
            var wfObjects = connection.ExecuteCommand<WFObject>(@"
            SELECT
                wfo.*,
            (SELECT wfot.GlobalName FROM wfobjecttype wfot WHERE wfot.Id = wfo.WfObjectTypeId) AS WFObjectTypeName
            FROM wfobject wfo WHERE wfo.ProjectId = @projectId AND wfo.Status = 1 
            ", projectId).ToList();
            return new SuccessDataResult<List<WFObject>>(wfObjects);
        }
    }
}

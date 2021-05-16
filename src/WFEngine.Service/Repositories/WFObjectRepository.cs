using System.Collections.Generic;
using System.Data;
using System.Linq;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;
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

        public IResult Insert(WFObject wfObject)
        {
            wfObject.Id = connection.Insert(wfObject);
            if (wfObject.Id < 1)
                return new ErrorResult(Messages.WFObject.NotCreateWFObject);
            return new SuccessResult();
        }

        public IDataResult<WFObject> FindWFObjectById(int id)
        {
            var wfObject = connection.ExecuteCommand<WFObject>("SELECT * FROM wfobject w WHERE w.Id = @id AND w.Status = 1", id)?.FirstOrDefault();
            if (wfObject == null)
                return new ErrorDataResult<WFObject>(null, Messages.WFObject.NotFoundWFObject);
            return new SuccessDataResult<WFObject>(wfObject);
        }

        public IResult Delete(WFObject wfObject)
        {
            var isDeleted = connection.Delete(wfObject);
            if (!isDeleted)
                return new ErrorResult(Messages.WFObject.NotDeletedWFObject);
            return new SuccessResult();
        }

        public IResult Update(WFObject wfObject)
        {
            var isUpdated = connection.Update(wfObject);
            if (!isUpdated)
                return new ErrorResult(Messages.WFObject.NotUpdatedWFObject);
            return new SuccessResult();
        }

        public IDataResult<List<WFObject>> GetWFObjects(List<int> projectIds)
        {
            string sql = @"SELECT
                wfo.*,
            (SELECT wfot.GlobalName FROM wfobjecttype wfot WHERE wfot.Id = wfo.WfObjectTypeId) AS WFObjectTypeName
            FROM wfobject wfo WHERE wfo.ProjectId IN ("+string.Join(',',projectIds)+") AND wfo.Status = 1 "; 
            var wfObjects = connection.ExecuteCommand<WFObject>(sql, projectIds).ToList();
            return new SuccessDataResult<List<WFObject>>(wfObjects);
        }
    }
}

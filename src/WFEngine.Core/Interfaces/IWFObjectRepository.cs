using System.Collections.Generic;
using WFEngine.Core.Entities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Core.Interfaces
{
    public interface IWFObjectRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        IDataResult<List<WFObject>> GetWFObjects(int projectId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectIds"></param>
        /// <returns></returns>
        IDataResult<List<WFObject>> GetWFObjects(List<int> projectIds);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfObject"></param>
        /// <returns></returns>
        IResult Insert(WFObject wfObject);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IDataResult<WFObject> FindWFObjectById(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfObject"></param>
        /// <returns></returns>
        IResult Delete(WFObject wfObject);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfObject"></param>
        /// <returns></returns>
        IResult Update(WFObject wfObject);
    }
}

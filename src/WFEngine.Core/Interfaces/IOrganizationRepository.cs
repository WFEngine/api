using WFEngine.Core.Entities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Core.Interfaces
{
    public interface IOrganizationRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDataResult<Organization> FindByName(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        IResult Insert(Organization organization);
     }
}

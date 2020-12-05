using System.Collections.Generic;
using WFEngine.Core.Entities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Core.Interfaces
{
    public interface ISolutionRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        IDataResult<Solution> FindSolutionByName(string name, int organizationId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IResult Insert(Solution solution, int userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IDataResult<List<Solution>> GetSolutions(int userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="solutionId"></param>
        /// <returns></returns>
        IDataResult<SolutionCollaborator> CheckSolutionCollaborator(int userId, int solutionId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IDataResult<Solution> FindSolutionById(int id);
    }
}

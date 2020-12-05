using System.Collections.Generic;
using WFEngine.Core.Entities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Core.Interfaces
{
    public interface IProjectRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="solutionId"></param>
        /// <returns></returns>
        DataResult<List<Project>> GetProjectFromSolutionId(int solutionId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        IResult Insert(Project project);
    }
}

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
        IDataResult<List<Project>> GetProjectFromSolutionId(int solutionId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="solutionIds"></param>
        /// <returns></returns>
        IDataResult<List<Project>> GetProjectFromSolutionIds(List<int> solutionIds);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        IResult Insert(Project project);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDataResult<List<ProjectType>> GetProjectTypes();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        IDataResult<Project> GetProject(int Id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        IResult Update(Project project);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        IResult Delete(Project project);        
    }
}

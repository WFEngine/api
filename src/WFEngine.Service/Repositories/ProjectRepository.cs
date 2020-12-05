using System.Collections.Generic;
using System.Data;
using System.Linq;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Service.Repositories
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        public ProjectRepository(IDbTransaction transaction) 
            : base(transaction)
        {

        }

        public DataResult<List<Project>> GetProjectFromSolutionId(int solutionId)
        {
            var result = connection.ExecuteCommand<Project>(@"
            SELECT * FROM project WHERE SolutionId = @solutionId AND Status = 1
            ", solutionId).ToList();
            return new SuccessDataResult<List<Project>>(result);
        }

        public IResult Insert(Project project)
        {
            project.Id = connection.Insert(project);
            if (project.Id > 0)
                return new SuccessResult();
            return new ErrorResult(Messages.Project.NotCreatedProject);            
        }
    }
}

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

        public IDataResult<List<Project>> GetProjectFromSolutionId(int solutionId)
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

        public IDataResult<List<ProjectType>> GetProjectTypes()
        {
            var projectTypes = connection.ExecuteCommand<ProjectType>("SELECT * FROM projecttype WHERE Status =1")?.ToList();
            return new SuccessDataResult<List<ProjectType>>(projectTypes);
        }

        public IDataResult<Project> GetProject(int Id)
        {
            var project = connection.ExecuteCommand<Project>(
                @"SELECT 
                    p.*,
                    (SELECT s.Name FROM solution s WHERE s.Id= p.SolutionId) as SolutionName,
                    (SELECT o.Name FROM organization o WHERE o.Id = p.OrganizationId) as OrganizationName
                FROM project p WHERE p.Id = @Id AND p.Status = 1", Id
                )?.FirstOrDefault();
            if (project == null)
                return new ErrorDataResult<Project>(null, Messages.Project.NotFoundProject);
            return new SuccessDataResult<Project>(project,"");
        }

        public IResult Update(Project project)
        {
            bool isUpdated = connection.Update(project);
            if (!isUpdated)
                return new ErrorResult(Messages.Project.NotUpdatedProject);
            return new SuccessResult();
        }

        public IResult Delete(Project project)
        {
            bool isDeleted = connection.Delete(project);
            if (isDeleted)
                return new SuccessResult();
            return new ErrorResult(Messages.Project.NotDeletedProject);
        }

        public IDataResult<List<Project>> GetProjectFromSolutionIds(List<int> solutionIds)
        {
            string sql = $"SELECT * FROM project WHERE SolutionId IN ({string.Join(',',solutionIds)}) AND Status = 1";
            var result = connection.ExecuteCommand<Project>(sql, solutionIds).ToList();
            return new SuccessDataResult<List<Project>>(result);
        }
    }
}

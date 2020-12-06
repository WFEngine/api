using System.Collections.Generic;
using System.Data;
using System.Linq;
using WFEngine.Core.Entities;
using WFEngine.Core.Enums;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Service.Repositories
{
    public class SolutionRepository : BaseRepository, ISolutionRepository
    {
        public SolutionRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        public IDataResult<Solution> FindSolutionByName(string name, int organizationId)
        {
            var solution = connection.ExecuteCommand<Solution>("SELECT * FROM solution WHERE Name = @name AND OrganizationId = @organizationId AND Status = 1", name, organizationId)?.FirstOrDefault();
            if (solution != null)
                return new SuccessDataResult<Solution>(solution);
            return new ErrorDataResult<Solution>(null, Messages.Solution.NotFoundSolution);
        }

        public IResult Insert(Solution solution, int userId)
        {
            solution.Id = connection.Insert(solution);
            if (solution.Id < 1)
                return new ErrorResult(Messages.Solution.NotCreatedSolution);
            SolutionCollaborator solutionCollaborator = new SolutionCollaborator()
            {
                SolutionId = solution.Id,
                UserId = userId,
                CollaboratorTypeId = enumCollaboratorType.OWNER,
                CreatorId = userId
            };
            solutionCollaborator.Id = connection.Insert(solutionCollaborator);
            if (solutionCollaborator.Id < 1)
                return new ErrorResult(Messages.Solution.NotCreatedSolution);
            return new SuccessResult();
        }
        public IDataResult<List<Solution>> GetSolutions(int userId)
        {
            var solutions = connection.ExecuteCommand<Solution>(@"SELECT 
            s.*,
            (SELECT o.Name FROM organization o WHERE o.Id = s.OrganizationId) AS OrganizationName,
            sc.CollaboratorTypeId
            FROM solutioncollaborator sc
            INNER JOIN solution s ON s.Id = sc.SolutionId
            WHERE sc.UserId = @userId AND sc.Status = 1 AND s.Status = 1;", userId).ToList();
            return new SuccessDataResult<List<Solution>>(solutions);
        }

        public IDataResult<SolutionCollaborator> CheckSolutionCollaborator(int userId, int solutionId)
        {
            var result = connection.ExecuteCommand<SolutionCollaborator>("SELECT * FROM solutioncollaborator WHERE SolutionId = @solutionId AND UserId = @userId AND Status = 1", userId, solutionId)?.FirstOrDefault();
            if (result != null)
                return new SuccessDataResult<SolutionCollaborator>(result);
            return new ErrorDataResult<SolutionCollaborator>(null,Messages.SolutionCollaborator.NotFoundSolutionCollaborator);
        }

        public IDataResult<Solution> FindSolutionById(int id)
        {
            var solution = connection.ExecuteCommand<Solution>(@"SELECT 
            s.*,
            (SELECT o.Name FROM organization o WHERE o.Id = s.OrganizationId) AS OrganizationName          
            FROM solution s WHERE s.Id = @id AND s.Status = 1", id)?.FirstOrDefault();
            if (solution != null)
                return new SuccessDataResult<Solution>(solution);
            return new ErrorDataResult<Solution>(null, Messages.Solution.NotFoundSolution);
        }

        public IResult DeleteSolution(Solution solution)
        {
            var isDeleted = connection.Delete(solution);
            if (isDeleted)
                return new SuccessResult();
            return new ErrorResult(Messages.Solution.NotDeletedSolution);
        }

        public IDataResult<List<SolutionCollaborator>> GetSolutionCollaborators(int solutionId)
        {
            var result = connection.ExecuteCommand<SolutionCollaborator>(@"SELECT  
            (SELECT ct.GlobalName FROM collaboratortype ct WHERE ct.Id = sc.CollaboratorTypeId) AS CollaboratorTypeName,
            (SELECT o.Name FROM organization o WHERE o.Id = u.OrganizationId) AS OrganizationName,
            u.name AS  UserName,
            u.email AS Email,
            u.avatar AS Avatar
            FROM solutioncollaborator sc
            INNER JOIN user u ON u.Id = sc.UserId
            WHERE sc.SolutionId = @solutionId AND sc.Status = 1;",solutionId).ToList();
            return new SuccessDataResult<List<SolutionCollaborator>>(result);
        }

        public IResult Update(Solution solution)
        {
            var isUpdated = connection.Update(solution);
            if (isUpdated)
                return new SuccessResult();
            return new ErrorResult(Messages.Solution.NotUpdatedSolution);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using WFEngine.Api.Dto.Request.Solution;
using WFEngine.Api.Dto.Response.Solution;
using WFEngine.Api.Filters;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Api.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    public class SolutionController : BaseController
    {
        readonly IStringLocalizer<SolutionResource> localizer;
        readonly IStringLocalizer<UserResource> userLocalizer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_baseLocalizer"></param>
        /// <param name="_localizer"></param>
        /// <param name="_userLocalizer"></param>
        public SolutionController(
            IUnitOfWork _uow,
            IMapper _mapper,
            IStringLocalizer<BaseResource> _baseLocalizer,
            IStringLocalizer<SolutionResource> _localizer,
            IStringLocalizer<UserResource> _userLocalizer
            )
            : base(_uow, _mapper, _baseLocalizer)
        {
            localizer = _localizer;
            userLocalizer = _userLocalizer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("insert")]
        public IActionResult Insert([FromBody] InsertSolutionRequestDTO dto)
        {
            InsertSolutionResponse baseResult = new InsertSolutionResponse();
            User user = CurrentUser;
            if (user == null)
                return NotFound(baseResult, userLocalizer[Messages.User.NotFoundUser]);
            Solution solution = mapper.Map<Solution>(dto);
            solution.OrganizationId = user.OrganizationId;
            IDataResult<Solution> solutionExists = uow.Solution.FindSolutionByName(dto.Name, user.OrganizationId);
            if (solutionExists.Success)
                return NotFound(baseResult, localizer[Messages.Solution.AlreadyExistsSolution]);
            IResult solutionCreated = uow.Solution.Insert(solution, user.Id);
            if (!solutionCreated.Success)
                return NotFound(baseResult, localizer[solutionCreated.Message]);
            if (!uow.Commit())
                return NotFound(baseResult);
            baseResult.Id = solution.Id;
            return Ok(baseResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("solutions")]
        public IActionResult GetSolutions()
        {
            SolutionsResponse solutionsResponse = new SolutionsResponse();
            User currentUser = CurrentUser;
            if (currentUser == null)
                return NotFound(solutionsResponse, userLocalizer[Messages.User.NotFoundUser]);
            solutionsResponse.Solutions = uow.Solution.GetSolutions(currentUser.Id).Data.Select(x => new SolutionsResponse.Solution
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CollaboratorTypeId = x.CollaboratorTypeId,
                OrganizationName = x.OrganizationName

            }).ToList();

            if (solutionsResponse.Solutions.Any())
            {
                List<ProjectType> projectTypes = uow.Project.GetProjectTypes().Data;
                foreach (var solution in solutionsResponse.Solutions)
                {
                    solution.Projects = uow.Project.GetProjectFromSolutionId(solution.Id).Data.Select(x => new SolutionsResponse.Project
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        ProjectType = projectTypes.FirstOrDefault(y => y.Id == x.ProjectTypeId)?.GlobalName
                    }).ToList();
                    solution.Colllaborators = uow.Solution.GetSolutionCollaborators(solution.Id).Data.Select(x => new SolutionsResponse.Colllaborator
                    {
                        CollaboratorTypeName = x.CollaboratorTypeName,
                        OrganizationName = x.OrganizationName,
                        UserName = x.UserName,
                        Email = x.Email,
                        Avatar = x.Avatar
                    }).ToList();
                }
            }

            return Ok(solutionsResponse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [WFSolutionOwner]
        public IActionResult Delete(int id)
        {
            DeleteSolutionResponse response = new DeleteSolutionResponse();
            IDataResult<Solution> solution = uow.Solution.FindSolutionById(id);
            if (!solution.Success)
                return NotFound(solution, localizer[solution.Message]);
            IResult solutionDeleted = uow.Solution.DeleteSolution(solution.Data);
            if (!solutionDeleted.Success)
                return NotFound(response, localizer[solutionDeleted.Message]);
            if (!uow.Commit())
                return NotFound(response);
            response.IsDeleted = true;
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("solution/{id}")]
        public IActionResult GetSolution(int id)
        {
            GetSolutionResponse response = new GetSolutionResponse();
            IDataResult<Solution> solutionExists = uow.Solution.FindSolutionById(id);
            if (!solutionExists.Success)
                return NotFound(response, localizer[solutionExists.Message]);
            var solution = solutionExists.Data;
            response.solution.Id = solution.Id;
            response.solution.Name = solution.Name;
            response.solution.Description = solution.Description;
            response.solution.OrganizationName = solution.OrganizationName;
            response.solution.CollaboratorTypeId = solution.CollaboratorTypeId;
            var projectTypes = uow.Project.GetProjectTypes().Data;
            response.solution.Projects = uow.Project.GetProjectFromSolutionId(id).Data.Select(x => new GetSolutionResponse.Project
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ProjectType = projectTypes.FirstOrDefault(y=>y.Id == x.ProjectTypeId)?.GlobalName
            }).ToList();
            response.solution.Colllaborators = uow.Solution.GetSolutionCollaborators(id).Data.Select(x => new GetSolutionResponse.Colllaborator
            {
                CollaboratorTypeName = x.CollaboratorTypeName,
                OrganizationName = x.OrganizationName,
                UserName = x.UserName,
                Email = x.Email,
                Avatar = x.Avatar
            }).ToList();
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update/{id}")]
        [WFSolutionOwner]
        public IActionResult Update(int id,[FromBody]UpdateSolutionRequestDTO dto)
        {
            UpdateSolutionResponse response = new UpdateSolutionResponse();
            IDataResult<Solution> solutionExists = uow.Solution.FindSolutionById(id);
            if (!solutionExists.Success)
                return NotFound(response, localizer[solutionExists.Message]);
            var solution = solutionExists.Data;
            solution.Name = dto.Name;
            solution.Description = dto.Description;
            IResult isUpdated = uow.Solution.Update(solution);
            if (!isUpdated.Success)
                return NotFound(response, localizer[isUpdated.Message]);
            if (!uow.Commit())
                return NotFound(response);
            response.IsUpdated = true;
            return Ok(response);
        }
    }
}

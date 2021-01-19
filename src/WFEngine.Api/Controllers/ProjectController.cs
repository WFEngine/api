using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Linq;
using WFEngine.Api.Dto.Request.Project;
using WFEngine.Api.Dto.Response.Project;
using WFEngine.Api.Filters;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectController : BaseController
    {
        readonly IStringLocalizer<ProjectResource> localizer;
        readonly IStringLocalizer<SolutionResource> solutionLocalizer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_baseLocalizer"></param>
        /// <param name="_localizer"></param>
        /// <param name="_solutionLocalizer"></param>
        public ProjectController(
            IUnitOfWork _uow,
            IMapper _mapper,
            IStringLocalizer<BaseResource> _baseLocalizer,
            IStringLocalizer<ProjectResource> _localizer,
            IStringLocalizer<SolutionResource> _solutionLocalizer
            )
            : base(_uow, _mapper, _baseLocalizer)
        {
            localizer = _localizer;
            solutionLocalizer = _solutionLocalizer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("projecttypes")]
        public IActionResult GetProjectTypes()
        {
            GetProjectTypesResponse baseResult = new GetProjectTypesResponse();
            baseResult.ProjectTypes = uow.Project.GetProjectTypes().Data.Select(x => new GetProjectTypesResponse.ProjectType
            {
                Id = x.Id,
                Name = x.Name,
                GlobalName = x.GlobalName
            }).ToList();
            return Ok(baseResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solutionId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("insert/{solutionId}")]
        [WFSolutionOwner]
        public IActionResult Insert(int solutionId, [FromBody] InsertProjectRequestDTO dto)
        {
            InsertProjectResponse projectResponse = new InsertProjectResponse();
            User user = CurrentUser;
            IDataResult<Solution> solutionExists = uow.Solution.FindSolutionById(solutionId);
            if (!solutionExists.Success)
                return NotFound(projectResponse, solutionLocalizer[solutionExists.Message]);
            Solution solution = solutionExists.Data;
            Project project = mapper.Map<Project>(dto);
            project.SolutionId = solution.Id;
            project.OrganizationId = solution.OrganizationId;
            project.CreatorId = user.Id;
            IResult projectCreated = uow.Project.Insert(project);
            if (!projectCreated.Success)
                return NotFound(projectResponse, localizer[projectCreated.Message]);
            if (!uow.Commit())
                return NotFound(projectResponse);
            projectResponse.Id = project.Id;
            return Ok(projectResponse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("get/{projectId}")]
        [WFProjectCollaborator]
        public IActionResult GetProject(int projectId)
        {
            GetProjectResponse response = new GetProjectResponse();
            IDataResult<Project> projectExists = uow.Project.GetProject(projectId);
            if (!projectExists.Success)
                return NotFound(response, localizer[projectExists.Message]);
            Project project = projectExists.Data;
            response.Project = new GetProjectResponse.ProjectItem()
            {
                Id = project.Id,
                UniqueKey = project.UniqueKey,
                Name = project.Name,
                Description = project.Description,
                OrganizationId = project.OrganizationId,
                OrganizationName = project.OrganizationName,
                ProjectTypeId = project.ProjectTypeId,
                SolutionId = project.SolutionId,
                SolutionName = project.SolutionName
            };
            return Ok(response);
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update/{id}")]
        [WFSolutionCollaboratorWrite]
        public IActionResult Update(int id,[FromBody]UpdateProjectRequestDTO dto)
        {
            UpdateProjectResponse response = new UpdateProjectResponse();
            IDataResult<Project> projectExists = uow.Project.GetProject(id);
            if (!projectExists.Success)
                return NotFound(response, localizer[projectExists.Message]);
            Project project = projectExists.Data;
            project.ProjectTypeId = dto.ProjectTypeId;
            project.Name = dto.Name;
            project.Description = dto.Description;
            IResult isUpdated = uow.Project.Update(project);
            if (!isUpdated.Success)
                return NotFound(response, localizer[isUpdated.Message]);
            if (!uow.Commit())
                return NotFound(response);
            response.IsUpdated = true;
            return Ok(response);
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
            //TODO: Proje Silme İşlemleri Kodlanacak
            return Ok(new { });
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Linq;
using WFEngine.Api.Dto.Response.Project;
using WFEngine.Api.Dto.Response.VariableType;
using WFEngine.Api.Filters;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;

namespace WFEngine.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class VariableTypeController : BaseController
    {
        readonly IStringLocalizer<VariableTypeResource> localizer;
        readonly IStringLocalizer<SolutionResource> solutionLocalizer;
        readonly IStringLocalizer<ProjectResource> projectLocalizer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_baseLocalizer"></param>
        /// <param name="_localizer"></param>
        /// <param name="_solutionLocalizer"></param>
        /// <param name="_projectLocalizer"></param>
        public VariableTypeController(
            IUnitOfWork _uow,
            IMapper _mapper,
            IStringLocalizer<BaseResource> _baseLocalizer,
            IStringLocalizer<VariableTypeResource> _localizer,
            IStringLocalizer<SolutionResource> _solutionLocalizer,
            IStringLocalizer<ProjectResource> _projectLocalizer
            )
            : base(_uow, _mapper, _baseLocalizer)
        {
            localizer = _localizer;
            solutionLocalizer = _solutionLocalizer;
            projectLocalizer = _projectLocalizer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("get/{projectId}")]
        [WFProjectCollaborator]
        public IActionResult Get(int projectId)
        {
            GetVariableTypeResponse response = new GetVariableTypeResponse();
            var projectExists = uow.Project.GetProject(projectId);
            if (!projectExists.Success)
                return NotFound(response, projectLocalizer[projectExists.Message]);
            Project project = projectExists.Data;
            var solutionExists = uow.Solution.FindSolutionById(project.SolutionId);
            if (!solutionExists.Success)
                return NotFound(response, solutionLocalizer[solutionExists.Message]);
            Solution solution = solutionExists.Data;
            response.VariableTypes = uow.VariableType.GetVariableTypes(solution.PackageVersionId).Data.Select(x => new GetVariableTypeResponse.VariableTypeItem
            {
                Id = x.Id,
                Type = x.Type,
                PackageVersionName = x.PackageVersionName
            }).ToList();
            return Ok(response);
        }
    }
}

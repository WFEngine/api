using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using WFEngine.Activities.Core.Enums;
using WFEngine.Api.Dto.Response.Activity;
using WFEngine.Api.Filters;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;

namespace WFEngine.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : BaseController
    {
        readonly IStringLocalizer<ActivityResource> localizer;
        readonly IStringLocalizer<ProjectResource> projectLocalizer;
        readonly IStringLocalizer<SolutionResource> solutionLocalizer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_baseLocalizer"></param>
        /// <param name="_localizer"></param>
        /// <param name="_projectLocalizer"></param>
        /// <param name="_solutionLocalizer"></param>
        public ActivityController(
            IUnitOfWork _uow,
            IMapper _mapper,
            IStringLocalizer<BaseResource> _baseLocalizer,
            IStringLocalizer<ActivityResource> _localizer,
            IStringLocalizer<ProjectResource> _projectLocalizer,
            IStringLocalizer<SolutionResource> _solutionLocalizer
            ) : base(_uow, _mapper, _baseLocalizer)
        {
            localizer = _localizer;
            projectLocalizer = _projectLocalizer;
            solutionLocalizer = _solutionLocalizer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param nam  e="projectId"></param>
        /// <returns></returns>
        [HttpGet("get/{projectId}")]
        [WFProjectCollaborator]
        public IActionResult GetActivities(int projectId)
        {            
            GetActivitiesResponse response = new GetActivitiesResponse();
            var projectExists = uow.Project.GetProject(projectId);
            if (!projectExists.Success)
                return NotFound(response, localizer[projectExists.Message]);
            Project project = projectExists.Data;
            var solutionExists = uow.Solution.FindSolutionById(project.SolutionId);
            if (!solutionExists.Success)
                return NotFound(response, solutionLocalizer[solutionExists.Message]);
            Solution solution = solutionExists.Data;
            var activityTypes = uow.ActivityType.GetActivityTypes().Data;
            var activities = uow.Activity.GetActivities(
                    activityTypes.Select(x => x.Id).ToList(),
                    new List<int>() { solution.PackageVersionId }
                ).Data;
            switch ((enumProjectType)project.ProjectTypeId)
            {
                case enumProjectType.Console:
                    activities.RemoveAll(x => x.IsPlatformBased && !x.ActivityName.StartsWith("WFEngine.Activities.Console"));
                    break;
                default:
                    break;
            }
            response.Activities = activityTypes.Select(x => new GetActivitiesResponse.ActivityTypeItem
            {
                Id = x.Id,
                Name = x.Name,
                Activities = activities.Where(y => y.ActivityTypeId == x.Id).Select(y => new GetActivitiesResponse.ActivityItem
                {
                    Id = y.Id,
                    Name = y.Name,
                    ActivityName = y.ActivityName,
                    AssemblyName = y.AssemblyName
                }).ToList()
            }).ToList();
            return Ok(response);
        }
    }
}

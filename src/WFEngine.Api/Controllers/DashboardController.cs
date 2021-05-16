using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WFEngine.Api.Dto.Response.Dashboard;
using WFEngine.Core.Interfaces;

namespace WFEngine.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class DashboardController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_baseLocalizer"></param>
        public DashboardController(
            IUnitOfWork _uow, 
            IMapper _mapper, 
            IStringLocalizer<BaseResource> _baseLocalizer
            )
            : base(_uow, _mapper, _baseLocalizer)
        {         
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult List()
        {
            ListDashboardResponse response = new ListDashboardResponse();
            var userId = CurrentUserId;
            var solutionExists  = uow.Solution.GetSolutions(userId);
            if (!solutionExists.Success)
                return NotFound(response);

            var solutions = solutionExists.Data;
            response.SolutionCount = solutions.Count;
            var projectExists = uow.Project.GetProjectFromSolutionIds(solutions.Select(f => f.Id).ToList());

            if (!projectExists.Success)
                return NotFound(projectExists);

            var projects = projectExists.Data;

            response.ProjectCount = projects.Count;

            var wfObjectExists = uow.WFObject.GetWFObjects(projects.Select(f => f.Id).ToList());

            if (!wfObjectExists.Success)
                return NotFound(response);

            var wfObjects = wfObjectExists.Data;

            response.WorkflowCount = wfObjects.Count;

            return Ok(response);
        }
    }
}

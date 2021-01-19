using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using WFEngine.Api.Utilities;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;
using WFEngine.Core.Utilities.Result;
using WFEngine.Service;

namespace WFEngine.Api.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class WFProjectCollaboratorAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        IStringLocalizer<BaseResource> baseLocalizer;
        /// <summary>
        /// 
        /// </summary>
        IStringLocalizer<SolutionResource> solutionLocalizer;
        /// <summary>
        /// 
        /// </summary>
        IStringLocalizer<ProjectResource> projectLocalizer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.Filters.Any(x => x.GetType() == typeof(WFSolutionOwnerAttribute)))
            {
                using (IUnitOfWork uow = new UnitOfWork())
                {
                    var httpContext = context.HttpContext;
                    var request = httpContext.Request;
                    baseLocalizer = (IStringLocalizer<BaseResource>)httpContext.RequestServices.GetService(typeof(IStringLocalizer<BaseResource>));
                    solutionLocalizer = (IStringLocalizer<SolutionResource>)httpContext.RequestServices.GetService(typeof(IStringLocalizer<SolutionResource>));
                    projectLocalizer = (IStringLocalizer<ProjectResource>)httpContext.RequestServices.GetService(typeof(IStringLocalizer<ProjectResource>));
                    object projectIdRouteValue = default;
                    if (request.RouteValues.ContainsKey("projectId"))
                        request.RouteValues.TryGetValue("projectId", out projectIdRouteValue);
                    else if (projectIdRouteValue == default && request.RouteValues.ContainsKey("id"))
                        request.RouteValues.TryGetValue("id", out projectIdRouteValue);

                    if (projectIdRouteValue == default)
                    {
                        IActionFilterResult.UnAuthorized<int>(context, baseLocalizer);
                        return;
                    }

                    int userId = JWTManager.GetUserId(httpContext, uow);
                    User currentUser = JWTManager.GetUser(userId, uow);
                    if (currentUser == null)
                    {
                        IActionFilterResult.UnAuthorized<int>(context, baseLocalizer);
                        return;
                    }

                    int projectId = int.Parse(projectIdRouteValue.ToString());
                    IDataResult<Project> projectExists = uow.Project.GetProject(projectId);
                    if (!projectExists.Success)
                    {
                        IActionFilterResult.UnAuthorized<int>(context, baseLocalizer, projectLocalizer[projectExists.Message]);
                        return;
                    }

                    IDataResult<SolutionCollaborator> isCollaborator = uow.Solution.CheckSolutionCollaborator(userId, projectExists.Data.SolutionId);
                    if (!isCollaborator.Success)
                    {
                        IActionFilterResult.UnAuthorized<int>(context, baseLocalizer, solutionLocalizer[isCollaborator.Message]);
                        return;
                    }
                }
            }
        }
    }
}

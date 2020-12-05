using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using System;
using WFEngine.Api.Utilities;
using WFEngine.Core.Entities;
using WFEngine.Core.Enums;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;
using WFEngine.Core.Utilities.Result;
using WFEngine.Service;

namespace WFEngine.Api.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class WFSolutionOwnerAttribute : Attribute, IActionFilter
    {
        IStringLocalizer<BaseResource> baseLocalizer;
        IStringLocalizer<SolutionResource> solutionLocalizer;

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
            using (IUnitOfWork uow = new UnitOfWork())
            {
                var httpContext = context.HttpContext;
                var request = httpContext.Request;
                baseLocalizer = (IStringLocalizer<BaseResource>)httpContext.RequestServices.GetService(typeof(IStringLocalizer<BaseResource>));
                solutionLocalizer = (IStringLocalizer<SolutionResource>)httpContext.RequestServices.GetService(typeof(IStringLocalizer<SolutionResource>));
                object solutionIdRouteValue = default;
                if (request.RouteValues.ContainsKey("solutionId"))
                    request.RouteValues.TryGetValue("solutionId", out solutionIdRouteValue);
                else if (solutionIdRouteValue == default && request.RouteValues.ContainsKey("id"))
                    request.RouteValues.TryGetValue("id", out solutionIdRouteValue);

                if (solutionIdRouteValue == default)
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
                int solutionId = int.Parse(solutionIdRouteValue.ToString());
                IDataResult<SolutionCollaborator> isCollaborator = uow.Solution.CheckSolutionCollaborator(userId, solutionId);
                if (!isCollaborator.Success)
                {
                    IActionFilterResult.UnAuthorized<int>(context, baseLocalizer, solutionLocalizer[isCollaborator.Message]);
                    return;
                }

                SolutionCollaborator solutionCollaborator = isCollaborator.Data;
                if (solutionCollaborator.CollaboratorTypeId != enumCollaboratorType.OWNER)
                {
                    IActionFilterResult.UnAuthorized<int>(context, baseLocalizer, solutionLocalizer[Messages.SolutionCollaborator.YouNotOwner]);
                    return;
                }
            }
        }
    }
}

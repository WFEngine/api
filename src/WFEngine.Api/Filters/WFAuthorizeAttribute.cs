using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using WFEngine.Api.Dto.Response.Auth;
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
    public class WFAuthorizeAttribute : Attribute, IActionFilter
    {
        IStringLocalizer<BaseResource> localizer;

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
            if (!context.Filters.Any(x => x.GetType() == typeof(WFAllowAnonymousAttribute)))
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {
                    localizer = (IStringLocalizer<BaseResource>)context.HttpContext.RequestServices.GetService(typeof(IStringLocalizer<BaseResource>));
                    var token = JWTManager.GetToken(context.HttpContext);
                    if (String.IsNullOrWhiteSpace(token))
                    {
                        IActionFilterResult.UnAuthorized<LoginResponse>(context, localizer);
                        return;
                    }
                    IDataResult<User> existUser = unitOfWork.User.CheckTokenWithUser(token);
                    if (!existUser.Success)
                    {
                        IActionFilterResult.UnAuthorized<LoginResponse>(context, localizer);
                        return;
                    }
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Net;
using WFEngine.Api.Dto.Response.Auth;
using WFEngine.Api.Models;
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
                        UnAuthorized(context);
                    IDataResult<User> existUser = unitOfWork.User.CheckTokenWithUser(token);
                    if (!existUser.Success)
                        UnAuthorized(context);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private async void UnAuthorized(ActionExecutingContext context)
        {
            BaseResult<LoginResponse> baseResult = new BaseResult<LoginResponse>();
            baseResult.Message = localizer[Messages.UnAuthorized];
            baseResult.StatusCode = HttpStatusCode.Unauthorized;
            context.Result = new UnauthorizedObjectResult(baseResult);
        }
    }
}

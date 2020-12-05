using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using System;
using System.Net;
using WFEngine.Api.Models;
using WFEngine.Core.Utilities;

namespace WFEngine.Api.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class IActionFilterResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="localizer"></param>
        public static void UnAuthorized<T>(ActionExecutingContext context, IStringLocalizer localizer, string message = "")
        {
            BaseResult<T> baseResult = new BaseResult<T>();
            if (String.IsNullOrEmpty(message))
                baseResult.Message = localizer[Messages.UnAuthorized];
            else
                baseResult.Message = message;
            baseResult.StatusCode = HttpStatusCode.Unauthorized;
            context.Result = new UnauthorizedObjectResult(baseResult);
        }
    }
}

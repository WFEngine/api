using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace WFEngine.Api.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class WFAllowAnonymousAttribute : Attribute, IActionFilter
    {
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
        }
    }
}

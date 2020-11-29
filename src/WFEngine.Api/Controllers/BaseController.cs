using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Net;
using WFEngine.Api.Filters;
using WFEngine.Api.Models;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;

namespace WFEngine.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [WFAuthorize]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly IUnitOfWork uow;
        /// <summary>
        /// 
        /// </summary>
        public readonly IMapper mapper;
        /// <summary>
        /// 
        /// </summary>
        public readonly IStringLocalizer<BaseResource> baseLocalizer;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_baseLocalizer"></param>
        public BaseController(
            IUnitOfWork _uow,
            IMapper _mapper,
            IStringLocalizer<BaseResource> _baseLocalizer
            )
        {
            uow = _uow;
            mapper = _mapper;
            baseLocalizer = _baseLocalizer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult Ok<T>(T data,string message= "")
        {
            BaseResult<T> baseResult = new BaseResult<T>()
            {
                Data = data,
                Message = message,
                StatusCode = HttpStatusCode.OK
            };

            if (String.IsNullOrEmpty(message))
                baseResult.Message = baseLocalizer[Messages.Successful];
            return new OkObjectResult(baseResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult NotFound<T>(T data,string message = "")
        {
            BaseResult<T> baseResult = new BaseResult<T>()
            {
                Data = data,
                Message = message,
                StatusCode = HttpStatusCode.NotFound
            };

            if (String.IsNullOrEmpty(message))
                baseResult.Message = baseLocalizer[Messages.Error];
            return new NotFoundObjectResult(baseResult);
        }
    }
}

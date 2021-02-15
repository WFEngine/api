using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Linq;
using WFEngine.Api.Dto.Response.PackageVersion;
using WFEngine.Core.Interfaces;

namespace WFEngine.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PackageVersionController : BaseController
    {
        readonly IStringLocalizer<PackageVersionResource> localizer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_baseLocalizer"></param>
        /// <param name="_localizer"></param>
        public PackageVersionController(
            IUnitOfWork _uow,
            IMapper _mapper,
            IStringLocalizer<BaseResource> _baseLocalizer,
            IStringLocalizer<PackageVersionResource> _localizer
            )
            : base(_uow, _mapper, _baseLocalizer)
        {
            localizer = _localizer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult List()
        {
            ListPackageVersionResponse response = new ListPackageVersionResponse();
            response.PackageVersions = uow.PackageVersion.GetPackageVersions().Data?.Select(x => new ListPackageVersionResponse.PackageVersion
            {
                Id = x.Id,
                Version = x.Version
            }).ToList();
            return Ok(response);
        }
    }
}

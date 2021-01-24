using AutoMapper;
using Microsoft.Extensions.Localization;
using WFEngine.Core.Interfaces;

namespace WFEngine.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class WFObjectController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        readonly IStringLocalizer<WFObjectResource> localizer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_baseLocalizer"></param>
        /// <param name="_localizer"></param>
        public WFObjectController(
            IUnitOfWork _uow,
            IMapper _mapper,
            IStringLocalizer<BaseResource> _baseLocalizer,
            IStringLocalizer<WFObjectResource> _localizer
            )
            : base(_uow, _mapper, _baseLocalizer)
        {
            localizer = _localizer;
        }
    }
}

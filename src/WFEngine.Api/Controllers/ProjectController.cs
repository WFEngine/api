using AutoMapper;
using Microsoft.Extensions.Localization;
using WFEngine.Core.Interfaces;

namespace WFEngine.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_baseLocalizer"></param>
        public ProjectController(IUnitOfWork _uow, IMapper _mapper, IStringLocalizer<BaseResource> _baseLocalizer) 
            : base(_uow, _mapper, _baseLocalizer)
        {
        }
    }
}

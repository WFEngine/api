using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Linq;
using WFEngine.Api.Dto.Request.Solution;
using WFEngine.Api.Dto.Response.Solution;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Api.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    public class SolutionController : BaseController
    {
        readonly IStringLocalizer<SolutionResource> localizer;
        readonly IStringLocalizer<UserResource> userLocalizer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_baseLocalizer"></param>
        /// <param name="_localizer"></param>
        /// <param name="_userLocalizer"></param>
        public SolutionController(
            IUnitOfWork _uow,
            IMapper _mapper,
            IStringLocalizer<BaseResource> _baseLocalizer,
            IStringLocalizer<SolutionResource> _localizer,
            IStringLocalizer<UserResource> _userLocalizer
            )
            : base(_uow, _mapper, _baseLocalizer)
        {
            localizer = _localizer;
            userLocalizer = _userLocalizer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("insert")]
        public IActionResult Insert([FromBody] InsertSolutionRequestDTO dto)
        {
            InsertSolutionResponse baseResult = new InsertSolutionResponse();
            User user = CurrentUser;
            if (user == null)
                return NotFound(baseResult, userLocalizer[Messages.User.NotFoundUser]);
            Solution solution = mapper.Map<Solution>(dto);
            solution.OrganizationId = user.OrganizationId;
            IDataResult<Solution> solutionExists = uow.Solution.FindByName(dto.Name, user.OrganizationId);
            if (solutionExists.Success)
                return NotFound(baseResult, localizer[Messages.Solution.AlreadyExistsSolution]);
            IResult solutionCreated = uow.Solution.Insert(solution, user.Id);
            if (!solutionCreated.Success)
                return NotFound(baseResult, localizer[solutionCreated.Message]);
            if (!uow.Commit())
                return NotFound(baseResult);
            baseResult.Id = solution.Id;
            return Ok(baseResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("solutions")]
        public IActionResult GetSolutions()
        {
            SolutionsResponse solutionsResponse = new SolutionsResponse();
            User currentUser = CurrentUser;
            if (currentUser == null)
                return NotFound(solutionsResponse, userLocalizer[Messages.User.NotFoundUser]);
            solutionsResponse.Solutions = uow.Solution.GetSolutions(currentUser.Id).Data.Select(x => new SolutionsResponse.Solution
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CollaboratorTypeId = x.CollaboratorTypeId,
                OrganizationName = x.OrganizationName                
                
            }).ToList();

            if (solutionsResponse.Solutions.Any())
            {
                //TODO : Get Projects
            }

            return Ok(solutionsResponse);
        }
    }
}

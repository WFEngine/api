using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RestSharp;
using WFEngine.Api.Dto.Request.Auth;
using WFEngine.Api.Dto.Response.Auth;
using WFEngine.Api.Models;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Api.Controllers
{
    /// <summary>
    /// Manages Session and User Transaction
    /// </summary>
    public class AuthController : BaseController
    {
        readonly IStringLocalizer<OrganizationResource> organizationLocalizer;

        readonly IStringLocalizer<UserResource> userLocalizer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_baseLocalizer"></param>
        /// <param name="_organizationLocalizer"></param>
        /// <param name="_userLocalizer"></param>
        public AuthController(
            IUnitOfWork _uow,
            IMapper _mapper,
            IStringLocalizer<BaseResource> _baseLocalizer,
            IStringLocalizer<OrganizationResource> _organizationLocalizer,
            IStringLocalizer<UserResource> _userLocalizer
            )
            : base(_uow, _mapper, _baseLocalizer)
        {
            organizationLocalizer = _organizationLocalizer;
            userLocalizer = _userLocalizer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult LogIn([FromBody] LoginRequestDTO dto)
        {
            BaseResult<LoginResponse> baseResult = new BaseResult<LoginResponse>();
            switch (dto.LoginType)
            {
                case Core.Enums.enumLoginType.Default:
                    break;
                case Core.Enums.enumLoginType.Github:
                    baseResult.Data.RedirectUrl = uow.User.GetGithubOAuthUrl();
                    break;
                default:
                    return NotFound(baseResult);
            }
            return Ok(baseResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDTO dto)
        {
            BaseResult<RegisterResponse> baseResult = new BaseResult<RegisterResponse>();
            IDataResult<Organization> organizationExists = uow.Organization.FindByName(dto.OrganizationName);
            if (organizationExists.Success)
                return NotFound(baseResult, organizationLocalizer[Messages.Organization.AlreadyExistsOrganization]);
            IDataResult<User> emailExists = uow.User.FindByEmail(dto.Email);
            if (emailExists.Success)
                return NotFound(baseResult, userLocalizer[Messages.User.AlreadyExistsEmail]);
            Organization organization = mapper.Map<Organization>(dto);
            IResult organizationCreated = uow.Organization.Insert(organization);
            if (!organizationCreated.Success)
                return NotFound(baseResult, organizationLocalizer[organizationCreated.Message]);
            User user = mapper.Map<User>(dto);
            user.OrganizationId = organization.Id;
            IResult userCreated = uow.User.Insert(user);
            if (!userCreated.Success)
                return NotFound(baseResult, userLocalizer[userCreated.Message]);
            if (!uow.Commit())
                return NotFound(baseResult);
            baseResult.Data.OrganizationId = organization.Id;
            baseResult.Data.UserId = user.Id;
            return Ok(baseResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet("github/authorize")]
        public IActionResult GithubAuthorize(string code, string state)
        {
            var client = new RestClient($"https://github.com/login/oauth/access_token?client_id=be3b22302d9c8e4a0224&client_secret=2c0fd1b3e5ea12bd536e515c7ae7aaaefd13d1fa&code={code}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cookie", "_octo=GH1.1.1836852379.1606056549; logged_in=no");
            IRestResponse response = client.Execute(request);            
            return Redirect("http://www.google.com");
        }
    }
}

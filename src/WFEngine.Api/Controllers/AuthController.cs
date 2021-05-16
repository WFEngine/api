using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using WFEngine.Api.Dto.Request.Auth;
using WFEngine.Api.Dto.Response.Auth;
using WFEngine.Api.Filters;
using WFEngine.Core.Entities;
using WFEngine.Core.Entities.Github;
using WFEngine.Core.Enums;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;
using WFEngine.Core.Utilities.Result;
using WFEngine.Service.Channels;

namespace WFEngine.Api.Controllers
{
    /// <summary>
    /// Manages Session and User Transaction
    /// </summary>
    public class AuthController : BaseController
    {
        readonly IStringLocalizer<OrganizationResource> organizationLocalizer;

        readonly IStringLocalizer<UserResource> userLocalizer;

        readonly RecoveryPasswordChannel passwordChannel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_uow"></param>
        /// <param name="_mapper"></param>
        /// <param name="_baseLocalizer"></param>
        /// <param name="_organizationLocalizer"></param>
        /// <param name="_userLocalizer"></param>
        /// <param name="_passwordChannel"></param>
        public AuthController(
            IUnitOfWork _uow,
            IMapper _mapper,
            IStringLocalizer<BaseResource> _baseLocalizer,
            IStringLocalizer<OrganizationResource> _organizationLocalizer,
            IStringLocalizer<UserResource> _userLocalizer,
            RecoveryPasswordChannel _passwordChannel
            )
            : base(_uow, _mapper, _baseLocalizer)
        {
            organizationLocalizer = _organizationLocalizer;
            userLocalizer = _userLocalizer;
            passwordChannel = _passwordChannel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [WFAllowAnonymous]
        public IActionResult LogIn([FromBody] LoginRequestDTO dto)
        {
            LoginResponse baseResult = new LoginResponse();
            switch (dto.LoginType)
            {
                case Core.Enums.enumLoginType.Default:
                    IDataResult<User> userExists = uow.User.FindByEmail(dto.Email);
                    if (!userExists.Success)
                        return NotFound(baseResult, userLocalizer[userExists.Message]);
                    var user = userExists.Data;
                    if (user.LoginTypeId != enumLoginType.Default)
                        return NotFound(baseResult, userLocalizer[Messages.User.NotFoundUser]);
                    IDataResult<Organization> organizationExists = uow.Organization.FindById(user.OrganizationId);
                    if (!organizationExists.Success)
                        return NotFound(baseResult, organizationLocalizer[organizationExists.Message]);
                    var organization = organizationExists.Data;
                    IResult loginUser = uow.User.LogIn(dto.Email, dto.Password);
                    if (!loginUser.Success)
                        return NotFound(baseResult, userLocalizer[loginUser.Message]);
                    mapper.Map(organization, baseResult);
                    mapper.Map(user, baseResult);
                    baseResult.Token = loginUser.Message;
                    baseResult.ExpireDate = DateTime.Now.AddDays(1);
                    break;
                case Core.Enums.enumLoginType.Github:
                    baseResult.RedirectUrl = uow.User.GetGithubOAuthUrl();
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
        [WFAllowAnonymous]
        public IActionResult Register([FromBody] RegisterRequestDTO dto)
        {
            RegisterResponse baseResult = new RegisterResponse();
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
            mapper.Map(organization, user);
            user.OrganizationId = organization.Id;
            IResult userCreated = uow.User.Insert(user);
            if (!userCreated.Success)
                return NotFound(baseResult, userLocalizer[userCreated.Message]);
            if (!uow.Commit())
                return NotFound(baseResult);
            mapper.Map(organization, baseResult);
            mapper.Map(user, baseResult);
            return Ok(baseResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet("github/authorize")]
        [WFAllowAnonymous]
        public IActionResult GithubAuthorize(string code, string state)
        {
            GithubOptions options = GithubOptions.Instance;
            ConnectionInfo connectionInfo = ConnectionInfo.Instance;
            var ouathClient = new RestClient($"https://github.com/login/oauth/access_token?client_id={options.ClientId}&client_secret={options.ClientSecret}&code={code}");
            ouathClient.Timeout = -1;
            var oauthRequest = new RestRequest(Method.POST);
            IRestResponse oauthResponse = ouathClient.Execute(oauthRequest);
            string designerUrl = connectionInfo.DesignerUrl + $"/auth/login";
            if (oauthResponse.StatusCode != HttpStatusCode.OK)
            {
                designerUrl += $"?status=error&token=";
                return Redirect(designerUrl);
            }
            AccessTokenGithub accessTokenGithub = JsonConvert.DeserializeObject<AccessTokenGithub>(oauthResponse.Content);
            var githubClient = new RestClient("https://api.github.com/user");
            githubClient.Timeout = -1;
            var githubRequest = new RestRequest(Method.GET);
            githubRequest.AddHeader("Authorization", $"Bearer {accessTokenGithub.access_token}");
            IRestResponse githubResponse = githubClient.Execute(githubRequest);
            if (githubResponse.StatusCode != HttpStatusCode.OK)
            {
                designerUrl += $"?status=error&token=";
                return Redirect(designerUrl);
            }
            UserGithub userGithub = JsonConvert.DeserializeObject<UserGithub>(githubResponse.Content);
            IDataResult<User> emailExists = uow.User.FindByEmail(userGithub.email);
            if (!emailExists.Success)
            {
                Organization organization = new Organization()
                {
                    Name = $"{userGithub.id}_{userGithub.login}"
                };

                IResult organizationCreated = uow.Organization.Insert(organization);
                if (!organizationCreated.Success)
                {
                    designerUrl += $"?status=error&token=";
                    return Redirect(designerUrl);
                }

                User user = new User()
                {
                    LanguageId = enumLanguage.EN,
                    LoginTypeId = enumLoginType.Github,
                    OrganizationId = organization.Id,
                    Name = userGithub.name,
                    Email = userGithub.email,
                    Avatar = userGithub.avatar_url,
                    PhoneNumber = "",
                    TwoFactorEnabled = false,
                    EmailVerificated = true
                };

                IResult userCreated = uow.User.Insert(user);
                if (!userCreated.Success)
                {
                    designerUrl += $"?status=error&token=";
                    return Redirect(designerUrl);
                }

                if (!uow.Commit())
                {
                    designerUrl += $"?status=error&token=";
                    return Redirect(designerUrl);
                }

            }
            if (emailExists.Success && emailExists.Data.LoginTypeId != enumLoginType.Github)
            {
                designerUrl += $"?status=error&token=";
                return Redirect(designerUrl);
            }
            IResult login = uow.User.LogIn(userGithub.email, "", accessTokenGithub.access_token);
            if (!login.Success)
                designerUrl += $"?status=error&token=";
            designerUrl += $"?status=success&token={accessTokenGithub.access_token}";
            return Redirect(designerUrl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getuser")]
        public IActionResult GetUser()
        {
            GetUserResponse baseResult = new GetUserResponse();
            int userId = JWTManager.GetUserId(HttpContext, uow);
            IDataResult<User> userExists = uow.User.FindById(userId);
            if (!userExists.Success)
                return NotFound(baseResult, userLocalizer[userExists.Message]);
            var user = userExists.Data;
            IDataResult<Organization> organizationExists = uow.Organization.FindById(user.OrganizationId);
            if (!organizationExists.Success)
                return NotFound(baseResult, organizationLocalizer[organizationExists.Message]);
            var organization = organizationExists.Data;
            mapper.Map(user, baseResult);
            mapper.Map(organization, baseResult);
            return Ok(baseResult);
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost("logout")]
        public IActionResult LogOut()
        {
            LogoutResponse baseResult = new LogoutResponse();
            User user = CurrentUser;
            if (user == null)
                return NotFound(baseResult, userLocalizer[Messages.User.NotFoundUser]);
            string currentToken = CurrentUserToken;
            if (String.IsNullOrEmpty(currentToken))
                return NotFound(baseResult, userLocalizer[Messages.User.NotFoundUser]);
            IResult logouted = uow.User.LogoutUser(currentToken);
            if (!logouted.Success)
                return NotFound(baseResult);
            baseResult.logouted = true;
            return Ok(baseResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("recoverypassword")]
        [WFAllowAnonymous]
        public IActionResult RecoveryPassword([FromBody] RecoveryPasswordRequestDTO dto)
        {
            RecoveryPasswordResponse response = new RecoveryPasswordResponse();

            var userExists = uow.User.FindByEmail(dto.Email);

            if (!userExists.Success)
                return NotFound(response, userLocalizer[userExists.Message]);

            var user = userExists.Data;

            if (user.LoginTypeId != enumLoginType.Default)
                return NotFound(response);

            passwordChannel.SendProducer(user.Email);

            response.IsEmailSending = true;

            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("changelanguage")]
        public IActionResult ChangeLanguage([FromBody]ChangeLanguageRequestDTO dto)
        {
            ChangeLanguageResponse response = new ChangeLanguageResponse();
            var userId = CurrentUserId;
            var userExists = uow.User.FindById(userId);
            if (!userExists.Success)
                return NotFound(response,userLocalizer[userExists.Message]);

            var user = userExists.Data;
            user.LanguageId = (enumLanguage)dto.LanguageId;

            var userUpdated = uow.User.UpdateUser(user);

            if (!userUpdated.Success)
                return NotFound(response, userLocalizer[userUpdated.Message]);

            if (!uow.Commit())
                return NotFound(response);

            response.IsLanguageChanged = userUpdated.Success;

            return Ok(response);
        }
    }
}

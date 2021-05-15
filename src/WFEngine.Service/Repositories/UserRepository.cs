using NETCore.Encrypt;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using WFEngine.Core.Entities;
using WFEngine.Core.Enums;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Service.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }

        public IDataResult<User> FindByEmail(string email)
        {
            var user = connection.ExecuteCommand<User>("SELECT * FROM user WHERE email = @email AND Status = 1", email)?.FirstOrDefault();
            if (user != null)
                return new SuccessDataResult<User>(user);
            return new ErrorDataResult<User>(null, Messages.User.NotFoundUser);
        }

        public IResult Insert(User user)
        {
            if (!String.IsNullOrEmpty(user.Password))
                user.Password = EncryptProvider.Md5(user.Password);
            user.Id = connection.Insert(user);
            if (user.Id > 0)
                return new SuccessResult();
            return new ErrorResult(Messages.User.NotCreatedUser);
        }

        public string GetGithubOAuthUrl()
        {
            GithubOptions options = GithubOptions.Instance;
            string url = $"https://github.com/login/oauth/authorize?client_id={options.ClientId}&redirect_uri={options.RedirectUrl}&login={options.Login}&scope={options.Scope}&state={options.State}&allow_signup={options.AllowSignUp}";
            return url;
        }

        public IResult LogIn(string email, string password = "", string token = "")
        {
            var user = connection.ExecuteCommand<User>("SELECT * FROM user WHERE email = @email AND Status = 1", email, password, token)?.FirstOrDefault();
            if (user == null)
                return new ErrorResult(Messages.User.NotFoundUser);
            var organization = connection.ExecuteCommand<Organization>($"SELECT * FROM organization WHERE Id = {user.OrganizationId}", email, password, token); if (!String.IsNullOrEmpty(password))
            {
                if (user.Password != EncryptProvider.Md5(password))
                    return new ErrorResult(Messages.User.LoginUnsuccessful);
            }
            if (String.IsNullOrEmpty(token))
            {
                token = JWTManager.GenerateToken(user);
            }

            try
            {
                var db = cache.GetDatabase((int)enumRedisDatabase.Tokens);
                db.StringSet($"{token}_user", JsonConvert.SerializeObject(user), TimeSpan.FromDays(1));
                db.StringSet($"{token}_organization", JsonConvert.SerializeObject(organization), TimeSpan.FromDays(1));
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.User.LoginUnsuccessful);
            }
            return new SuccessResult(token);
        }

        public IDataResult<User> CheckTokenWithUser(string token)
        {
            var db = cache.GetDatabase((int)enumRedisDatabase.Tokens);
            if (!db.KeyExists($"{token}_user"))
                return new ErrorDataResult<User>(null, Messages.User.NotFoundUser);
            var keyValue = db.StringGet($"{token}_user");
            if(String.IsNullOrEmpty(keyValue))
                return new ErrorDataResult<User>(null, Messages.User.NotFoundUser);
            var user = JsonConvert.DeserializeObject<User>(keyValue);
            return new SuccessDataResult<User>(user);            
        }

        public IDataResult<User> FindById(int id)
        {
            var user = connection.ExecuteCommand<User>("SELECT * FROM user WHERE Id = @id AND Status = 1", id)?.FirstOrDefault();
            if (user != null)
                return new SuccessDataResult<User>(user);
            return new ErrorDataResult<User>(user, Messages.User.NotFoundUser);
        }

        public IResult LogoutUser(string token)
        {
            try
            {
                var db = cache.GetDatabase((int)enumRedisDatabase.Tokens);
                db.KeyDelete($"{token}_organization");
                db.KeyDelete($"{token}_user");
                return new SuccessResult();
            }
            catch (Exception)
            {
                return new ErrorResult();
            }
        }

        public IResult UpdateUser(User user)
        {
            var isSuccess = connection.Update(user);
            if (isSuccess)
                return new SuccessResult();
            return new ErrorResult(Messages.User.UserNotUpdated);
        }
    }
}

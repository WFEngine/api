using NETCore.Encrypt;
using System.Data;
using System.Linq;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Service.Repositories
{
    public class UserRepository : BaseRepository,IUserRepository
    {
        public UserRepository(IDbTransaction transaction) 
            : base(transaction)
        {

        }

        public IDataResult<User> FindByEmail(string email)
        {
            var user = connection.ExecuteCommand<User>("SELECT * FROM user WHERE email = @email AND Status = 1",email)?.FirstOrDefault();
            if (user != null)
                return new SuccessDataResult<User>(user);
            return new ErrorDataResult<User>(null, Messages.User.NotFoundUser);
        }     

        public IResult Insert(User user)
        {
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
    }
}

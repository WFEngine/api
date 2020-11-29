using WFEngine.Core.Entities;
using WFEngine.Core.Utilities.Result;

namespace WFEngine.Core.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        IDataResult<User> FindByEmail(string email);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IResult Insert(User user);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetGithubOAuthUrl();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        IResult LogIn(string email, string password = "", string token = "");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        IDataResult<User> CheckTokenWithUser(string token);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IDataResult<User> FindById(int id);
    }
}

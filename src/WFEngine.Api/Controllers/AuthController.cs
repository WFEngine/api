using Microsoft.AspNetCore.Mvc;

namespace WFEngine.Api.Controllers
{
    /// <summary>
    /// Manages Session and User Transaction
    /// </summary>
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        public IActionResult LogIn()
        {
            return new JsonResult(new { });
        }

        [HttpPost("register")]
        public IActionResult Register()
        {
            return new JsonResult(new { });
        }
    }
}

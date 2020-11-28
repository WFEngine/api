using FluentValidation;
using WFEngine.Core.Enums;

namespace WFEngine.Api.Dto.Request.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginRequestDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public enumLoginType LoginType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LoginRequestDTOValidator : AbstractValidator<LoginRequestDTO>
    {
        /// <summary>
        /// 
        /// </summary>
        public LoginRequestDTOValidator()
        {
            RuleFor(x => x.LoginType).Must((value) => value > 0).WithMessage($"Login Type Greater Than 0");
            When(x => x.LoginType == enumLoginType.Default, () =>
               {
                   RuleFor(y => y.Email).NotEmpty().MaximumLength(100).EmailAddress();
                   RuleFor(y => y.Password).NotEmpty().MaximumLength(50);
               });
        }
    }
}

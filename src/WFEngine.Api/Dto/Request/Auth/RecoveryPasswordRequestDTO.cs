using FluentValidation;

namespace WFEngine.Api.Dto.Request.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public class RecoveryPasswordRequestDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RecoveryPasswordRequestValidator
        : AbstractValidator<RecoveryPasswordRequestDTO>
    {
        /// <summary>
        /// 
        /// </summary>
        public RecoveryPasswordRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}

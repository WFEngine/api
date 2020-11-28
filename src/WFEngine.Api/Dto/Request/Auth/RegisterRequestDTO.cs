using FluentValidation;
using System;
using System.Text.RegularExpressions;
using WFEngine.Core.Utilities;

namespace WFEngine.Api.Dto.Request.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public class RegisterRequestDTO
    {
        /// <summary>
        /// User Organization Name
        /// </summary>
        public string OrganizationName { get; set; }
        /// <summary>
        /// User Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User Email Address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// User Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// User Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// User Avatar
        /// </summary>
        public string Avatar { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RegisterRequestDTOValidator : AbstractValidator<RegisterRequestDTO>
    {
        /// <summary>
        /// 
        /// </summary>
        public RegisterRequestDTOValidator()
        {
            RuleFor(x => x.OrganizationName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().MaximumLength(100).EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
            RuleFor(x => x.PhoneNumber).Must((value) =>
            {
                bool isValid = true;
                if (!String.IsNullOrEmpty(value))
                {
                    Regex regex = new Regex(Regexs.PhoneNumberRegex);
                    isValid = regex.IsMatch(value);
                }
                return isValid;
            }).WithMessage("Phone Number Not Valid");
            RuleFor(x => x.Avatar).Must((value) =>
            {
                bool isValid = true;
                if (!String.IsNullOrEmpty(value))
                {
                    Regex regex = new Regex(Regexs.UrlRegex);
                    isValid = regex.IsMatch(value);
                }
                return isValid;
            }).WithMessage("Url Not Valid");
        }
    }
}

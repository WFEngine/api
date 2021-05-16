using FluentValidation;
using System;
using WFEngine.Core.Enums;

namespace WFEngine.Api.Dto.Request.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public class ChangeLanguageRequestDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public int LanguageId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ChangeLanguageRequestValidator : AbstractValidator<ChangeLanguageRequestDTO>
    {
        /// <summary>
        /// 
        /// </summary>
        public ChangeLanguageRequestValidator()
        {
            RuleFor(x => x.LanguageId).NotNull().Must(instance =>
            {
                try
                {
                    enumLanguage lang = (enumLanguage)Enum.Parse(typeof(enumLanguage), instance.ToString());
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }
    }
}

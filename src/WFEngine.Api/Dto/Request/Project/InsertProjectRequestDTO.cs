using FluentValidation;
using System;

namespace WFEngine.Api.Dto.Request.Project
{
    /// <summary>
    /// 
    /// </summary>
    public class InsertProjectRequestDTO
    {       
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class InsertProjectRequestValidator : AbstractValidator<InsertProjectRequestDTO>
    {
        /// <summary>
        /// 
        /// </summary>
        public InsertProjectRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            When(x => !String.IsNullOrEmpty(x.Description), () =>
            {
                RuleFor(x => x.Description).MaximumLength(255);
            });
        }
    }
}

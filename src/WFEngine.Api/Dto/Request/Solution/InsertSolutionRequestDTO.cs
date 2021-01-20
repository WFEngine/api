using FluentValidation;
using System;

namespace WFEngine.Api.Dto.Request.Solution
{
    /// <summary>
    /// 
    /// </summary>
    public class InsertSolutionRequestDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PackageVersionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class InsertSolutionRequestValidator
        : AbstractValidator<InsertSolutionRequestDTO>
    {
        /// <summary>
        /// 
        /// </summary>
        public InsertSolutionRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.PackageVersionId).GreaterThan(0);
            When(x => !String.IsNullOrEmpty(x.Description), () =>
               {
                   RuleFor(x => x.Description).MaximumLength(255);
               });
        }
    }
}

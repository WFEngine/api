using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WFEngine.Api.Dto.Request.Project
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateProjectRequestDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ProjectTypeId { get; set; }
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
    public class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequestDTO>
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdateProjectRequestValidator()
        {
            RuleFor(x => x.ProjectTypeId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            When(x => !String.IsNullOrEmpty(x.Description), () =>
            {
                RuleFor(x => x.Description).MaximumLength(255);
            });
        }
    }
}

﻿using FluentValidation;
using System;

namespace WFEngine.Api.Dto.Request.Solution
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateSolutionRequestDTO
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
    public class UpdateSolutionRequestValidator : AbstractValidator<UpdateSolutionRequestDTO>
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdateSolutionRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            When(x => !String.IsNullOrEmpty(x.Description), () =>
            {
                RuleFor(x => x.Description).MaximumLength(255);
            });
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WFEngine.Api.Dto.Request.WFObject
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateWFObjectRequestDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UpdateWFObjectRequestDTO()
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UpdateWFObjectRequestValidator  : AbstractValidator<UpdateWFObjectRequestDTO>
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdateWFObjectRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}

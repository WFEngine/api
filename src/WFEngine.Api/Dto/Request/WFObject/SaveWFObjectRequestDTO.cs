using FluentValidation;
using Newtonsoft.Json;
using System;
using WFEngine.Activities.Core.Model;

namespace WFEngine.Api.Dto.Request.WFObject
{
    /// <summary>
    /// 
    /// </summary>
    public class SaveWFObjectRequestDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }

        public SaveWFObjectRequestDTO()
        {

        }
    }

    public class SaveWFObjectRequestDTOValidator : AbstractValidator<SaveWFObjectRequestDTO>
    {
        /// <summary>
        /// 
        /// </summary>
        public SaveWFObjectRequestDTOValidator()
        {
            RuleFor(x => x.Content).NotEmpty().Must(x =>
            {
                try
                {
                    WFWorkflow m = JsonConvert.DeserializeObject<WFWorkflow>(x);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
    }
}

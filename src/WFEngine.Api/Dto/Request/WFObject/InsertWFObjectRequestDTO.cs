using FluentValidation;

namespace WFEngine.Api.Dto.Request.WFObject
{
    /// <summary>
    /// 
    /// </summary>
    public class InsertWFObjectRequestDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public int WfObjectTypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public InsertWFObjectRequestDTO()
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class InsertWFObjectRequestValidator : AbstractValidator<InsertWFObjectRequestDTO>
    {
        /// <summary>
        /// 
        /// </summary>
        public InsertWFObjectRequestValidator()
        {
            RuleFor(x => x.WfObjectTypeId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}

using System;

namespace WFEngine.Api.Dto.Response.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public int OrganizationId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizationName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool TwoFactorEnabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool EmailVerificated { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LanguageId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ExpireDate{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RedirectUrl { get; set; }
    }
}

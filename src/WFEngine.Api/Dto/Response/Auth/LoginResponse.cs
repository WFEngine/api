using System;

namespace WFEngine.Api.Dto.Response.Auth
{
    public class LoginResponse
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool EmailVerificated { get; set; }
        public string Token { get; set; }
        public DateTime? ExpireDate{ get; set; }
        public string RedirectUrl { get; set; }
    }
}

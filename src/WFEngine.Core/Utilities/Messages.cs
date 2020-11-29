namespace WFEngine.Core.Utilities
{
    public static class Messages
    {
        public static string Successful => "Successful";
        public static string Error => "Error";

        public static string UnAuthorized => "UnAuthorized";

        public sealed class Organization
        {
            public static string NotFoundOrganization => "NotFoundOrganization";
            public static string AlreadyExistsOrganization => "AlreadyExistsOrganization";
            public static string NotCreatedOrganization => "NotCreatedOrganization";
        }

        public sealed class User
        {
            public static string NotFoundUser => "NotFoundUser";
            public static string AlreadyExistsEmail => "AlreadyExistsEmail";
            public static string NotCreatedUser => "NotCreatedUser";
            public static string LoginUnsuccessful => "LoginUnsuccessful";
        }
    }
}

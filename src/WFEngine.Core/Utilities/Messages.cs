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
            public static string UserNotUpdated => "UserNotUpdated";
        }

        public sealed class Solution
        {
            public static string NotFoundSolution => "NotFoundSolution";
            public static string AlreadyExistsSolution => "AlreadyExistsSolution";
            public static string NotCreatedSolution => "NotCreatedSolution";
            public static string NotDeletedSolution => "NotDeletedSolution";
            public static string NotUpdatedSolution => "NotUpdatedSolution";
        }

        public sealed class SolutionCollaborator
        {
            public static string NotFoundSolutionCollaborator => "NotFoundSolutionCollaborator";
            public static string YouNotOwner => "YouNotOwner";
        }

        public sealed class Project
        {
            public static string NotCreatedProject => "NotCreatedProject";
            public static string NotFoundProject => "NotFoundProject";
            public static string NotUpdatedProject => "NotUpdatedProject";
            public static string NotDeletedProject => "NotDeletedProject";
        }

        public sealed class WFObject
        {
            public static string NotCreateWFObject => "NotCreatedWFObject";
            public static string NotFoundWFObject => "NotFoundWFObject";
            public static string NotDeletedWFObject => "NotDeletedWFObject";
            public static string NotUpdatedWFObject => "NotUpdatedWFObject";
        }
    }
}

using Dapper.Contrib.Extensions;
using WFEngine.Core.Enums;

namespace WFEngine.Core.Entities
{
    [Table("solutioncollaborator")]
    public class SolutionCollaborator :BaseEntity
    {
        public int SolutionId { get; set; }
        public int UserId { get; set; }
        public enumCollaboratorType CollaboratorTypeId { get; set; }

        [Write(false)]
        public string CollaboratorTypeName { get; set; }

        [Write(false)]
        public string OrganizationName{ get; set; }

        [Write(false)]
        public string UserName { get; set; }

        [Write(false)]
        public string Email { get; set; }

        [Write(false)]
        public string Avatar { get; set; }
    }
}

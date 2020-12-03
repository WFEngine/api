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
    }
}

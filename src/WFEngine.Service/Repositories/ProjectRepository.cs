using System.Data;
using WFEngine.Core.Interfaces;

namespace WFEngine.Service.Repositories
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        public ProjectRepository(IDbTransaction transaction) : base(transaction)
        {

        }
    }
}

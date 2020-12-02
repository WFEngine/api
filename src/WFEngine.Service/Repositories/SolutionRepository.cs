using System.Data;
using WFEngine.Core.Interfaces;

namespace WFEngine.Service.Repositories
{
    public class SolutionRepository : BaseRepository, ISolutionRepository
    {
        public SolutionRepository(IDbTransaction transaction) : base(transaction)
        {

        }
    }
}

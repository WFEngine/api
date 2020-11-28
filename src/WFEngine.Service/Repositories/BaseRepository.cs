using StackExchange.Redis;
using System.Data;
using WFEngine.Core.Interfaces;

namespace WFEngine.Service.Repositories
{
    public class BaseRepository
    {
        private IDbTransaction _transaction { get; set; }

        private IDbContext _instance { get; set; }

        private ConnectionMultiplexer _cache { get; set; }

        protected IDbContext connection
        {
            get
            {
                return _instance ?? (_instance = new DbContext(_transaction));
            }
        }

        protected ConnectionMultiplexer cache
        {
            get
            {
                return _cache;
            }
        }

        public BaseRepository(IDbTransaction transaction)
        {
            this._transaction = transaction;
        }
    }
}

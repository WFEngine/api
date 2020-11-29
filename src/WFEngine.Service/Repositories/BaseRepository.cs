using StackExchange.Redis;
using System.Data;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;

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
                if(_cache == null)
                {
                    var connectionIfo = ConnectionInfo.Instance;
                    _cache = ConnectionMultiplexer.Connect(connectionIfo.RedisConnectionString);
                }
                return _cache;
            }
        }

        public BaseRepository(IDbTransaction transaction)
        {
            this._transaction = transaction;
        }
    }
}

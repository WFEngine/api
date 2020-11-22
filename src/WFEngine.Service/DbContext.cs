using System.Collections.Generic;
using WFEngine.Core.Entities;
using WFEngine.Core.Interfaces;

namespace WFEngine.Service
{
    public class DbContext : IDbContext
    {
        public bool Delete<T>(T item) where T : BaseEntity, new()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> ExecuteCommand<T>(string sql, params object[] args)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> ExecuteCommand<T>(string sql)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> ExecuteProcedure<T>(string sql, params object[] args)
        {
            throw new System.NotImplementedException();
        }

        public T GetByID<T>(long id) where T : class, new()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetList<T>() where T : class, new()
        {
            throw new System.NotImplementedException();
        }

        public int Insert<T>(T item) where T : class, new()
        {
            throw new System.NotImplementedException();
        }

        public bool Update<T>(T item) where T : BaseEntity, new()
        {
            throw new System.NotImplementedException();
        }
    }
}

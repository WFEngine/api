using System;
using WFEngine.Core.Interfaces;

namespace WFEngine.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        public bool Commit()
        {
            throw new NotImplementedException();
        }      

        public bool Rollback()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

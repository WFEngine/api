using System;

namespace WFEngine.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
        bool Rollback();
    }
}

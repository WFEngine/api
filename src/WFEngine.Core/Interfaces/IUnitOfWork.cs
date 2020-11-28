using System;

namespace WFEngine.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOrganizationRepository Organization {get;}
        IUserRepository User { get; }
        bool Commit();
        bool Rollback();
    }
}

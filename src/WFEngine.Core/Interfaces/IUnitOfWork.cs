using System;

namespace WFEngine.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOrganizationRepository Organization {get;}
        IUserRepository User { get; }
        ISolutionRepository Solution { get; }
        IProjectRepository Project { get; }
        bool Commit();
        bool Rollback();
    }
}

using System;

namespace WFEngine.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOrganizationRepository Organization {get;}
        IUserRepository User { get; }
        ISolutionRepository Solution { get; }
        IProjectRepository Project { get; }
        IPackageVersionRepository PackageVersion { get; }
        IWFObjectRepository WFObject { get; }

        bool Commit();
        bool Rollback();
    }
}

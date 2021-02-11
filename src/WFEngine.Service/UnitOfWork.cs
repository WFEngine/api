using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;
using WFEngine.Service.Repositories;

namespace WFEngine.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        IDbTransaction transaction;
        IDbConnection connection;

        IOrganizationRepository _organization;
        IUserRepository _user;
        ISolutionRepository _solution;
        IProjectRepository _project;
        IPackageVersionRepository _packageVersion;
        IWFObjectRepository _wfObjectRepository;
        IActivityRepository _activityRepository;
        IActivityTypeRepository _activityTypeRepository;

        bool disposed;


        public UnitOfWork()
        {
            try
            {
                ConnectionInfo connectionInfo = ConnectionInfo.Instance;
                connection = new MySqlConnection(connectionInfo.MySQLConnectionString);
                connection.Open();
                transaction = connection.BeginTransaction();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public IOrganizationRepository Organization
        {
            get
            {
                return _organization ?? (_organization = new OrganizationRepository(transaction));
            }
        }

        public IUserRepository User
        {
            get
            {
                return _user ?? (_user = new UserRepository(transaction));
            }
        }

        public ISolutionRepository Solution
        {
            get
            {
                return _solution ?? (_solution = new SolutionRepository(transaction));
            }
        }

        public IProjectRepository Project
        {
            get
            {
                return _project ?? (_project = new ProjectRepository(transaction));
            }
        }

        public IPackageVersionRepository PackageVersion
        {
            get
            {
                return _packageVersion ?? (_packageVersion = new PackageVersionRepository(transaction));
            }
        }

        public IWFObjectRepository WFObject
        {
            get
            {
                return _wfObjectRepository ?? (_wfObjectRepository = new WFObjectRepository(transaction));
            }
        }

        public IActivityRepository Activity
        {
            get
            {
                return _activityRepository ?? (_activityRepository = new ActivityRepository(transaction));
            }
        }

        public IActivityTypeRepository ActivityType
        {
            get
            {
                return _activityTypeRepository ?? (_activityTypeRepository = new ActivityTypeRepository(transaction));
            }
        }

        public bool Commit()
        {
            bool rtn = false;
            try
            {
                transaction.Commit();
                rtn = true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
                transaction = connection.BeginTransaction();
                resetRepositories();
            }
            return rtn;
        }


        public bool Rollback()
        {
            bool rtn = false;
            try
            {
                transaction?.Rollback();
                rtn = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                transaction?.Dispose();
                transaction = connection.BeginTransaction();
                resetRepositories();
            }
            return rtn;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void resetRepositories()
        {
            _organization = null;
            _user = null;
            _solution = null;
            _project = null;
            _packageVersion = null;
            _wfObjectRepository = null;
            _activityRepository = null;
            _activityTypeRepository = null;
        }

        private void dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (transaction != null)
                    {
                        transaction.Dispose();
                        transaction = null;
                    }

                    if (connection != null)
                    {
                        connection.Dispose();
                        connection = null;
                    }
                }
                disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}

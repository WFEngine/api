using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;

namespace WFEngine.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        IDbTransaction transaction;
        IDbConnection connection;

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

using Microsoft.Data.Sqlite;
using System;
using System.Data;

namespace LibraryInfrastructure.Shared
{
    // disposable repository base class
    // there might be a better way
    public class RepositoryBase : IDisposable
    {
        internal IDbConnection dbConnection;
        private bool isDisposed;

        public RepositoryBase()
        {
            // create connection
            // TODO: data source
            var sqlConnectionSb = new SqliteConnectionStringBuilder { DataSource = "test.sqlite" };
            var connection = new SqliteConnection(sqlConnectionSb.ToString());
            this.dbConnection = connection;
            this.isDisposed = false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    dbConnection.Dispose();
                    isDisposed = false;
                    dbConnection = null;
                }
            }
        }

        ~RepositoryBase()
            => Dispose(disposing: false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

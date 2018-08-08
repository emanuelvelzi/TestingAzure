using TestingAzure.DataAccess.Interfaces;

namespace TestingAzure.DataAccess.EntityFramework
{
    public class ConnectionString : IConnectionString
    {
        string IConnectionString.ConnectionString { get; set; }

        public ConnectionString(string connectionString)
        {
            ((IConnectionString)this).ConnectionString = connectionString;
        }
    }
}

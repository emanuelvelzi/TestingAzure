using NHibernate.Driver;
using System.Data.Common;

namespace TestingAzure.DataAccess.NHibernate
{
    /// <summary>
    /// Helper driver to intercept queries against database and debug more easily.
    /// </summary>
    public class LoggerClientDriver : OracleManagedDataClientDriver
    {
        public override void AdjustCommand(DbCommand command)
        {
#if DEBUG
            var query = command.CommandText;
            foreach (DbParameter parameter in command.Parameters)
            {
                query = query.Replace(parameter.ParameterName, parameter.Value.ToString());
            }
#endif
            return;
        }
    }
}
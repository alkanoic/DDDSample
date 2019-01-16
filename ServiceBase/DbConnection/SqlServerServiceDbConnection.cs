using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBase.DbConnection
{
    public class SqlServerServiceDbConnection : IServiceDbConnection
    {
        public string ConnectionString { get; set; }

        public IDbConnection CreateDbConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryBase
{
    public abstract class RepositoryBase
    {

        public IDbConnection DbConnection { get; set; }

    }
}

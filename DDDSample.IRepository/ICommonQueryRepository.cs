using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDSample.IRepository
{
    public interface ICommonQueryRepository<TEntity, TKey>
    {

        IDbConnection DbConnection { get; set; }

        IEnumerable<TEntity> SelectAll();

        TEntity SelectById(TKey key);

    }
}

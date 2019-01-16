using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDSample.IRepository
{
    public interface ICommonCommandRepository<TEntity, TKey>
    {

        IDbConnection DbConnection { get; set; }

        IDbTransaction DbTransaction { get; set; }

        int Insert(TEntity entity);

        int Update(TEntity entity);

        int Delete(TKey key);
    }
}

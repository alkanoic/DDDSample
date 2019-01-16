using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDSample.DataModel.Tables;

namespace DDDSample.Service.Query
{
    public interface IUserQueryService
    {
        IEnumerable<User> SelectAll();
    }
}

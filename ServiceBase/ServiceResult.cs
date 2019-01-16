using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBase
{
    public class ServiceResult<TResult>
    {

        public bool Success { get; set; }

        public Exception OccuredException { get; set; }

        public TResult Result { get; set; }
    }
}

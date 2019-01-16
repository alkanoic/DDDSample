using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBase
{
    public class ServiceException : Exception
    {

        public ServiceException(string message) : base(message)
        {

        }

        public ServiceException(string message, Exception ex) : base(message, ex)
        {

        }

    }
}

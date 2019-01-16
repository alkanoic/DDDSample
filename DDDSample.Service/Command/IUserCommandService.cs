using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDSample.DataModel.Tables;

namespace DDDSample.Service.Command
{
    public interface IUserCommandService
    {
        void Insert(ServiceModel.InsertUserModel u);

    }
}

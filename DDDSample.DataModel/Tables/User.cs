using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDSample.DataModel.Tables
{
    public class User : BaseDataModel
    {
        public virtual Guid UserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual int UserStatus { get; set; }

        public override string ToString()
        {
            return $"{this.GetType().ToString()}\t{nameof(UserId)}\t{UserId}\t{nameof(UserName)}\t{UserName}\t{nameof(UserStatus)}\t{UserStatus}";
        }
    }
}

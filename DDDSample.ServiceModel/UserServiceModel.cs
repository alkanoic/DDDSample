using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDSample.ServiceModel
{
    public class InsertUserModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        public int UserStatus { get; set; }

        public override string ToString()
        {
            return $"{this.GetType().ToString()}\t{nameof(UserId)}\t{UserId}\t{nameof(UserName)}\t{UserName}\t{nameof(UserStatus)}\t{UserStatus}";
        }
    }
}

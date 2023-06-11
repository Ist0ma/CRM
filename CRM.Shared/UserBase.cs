using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Shared
{
    public class BaseUser
    {
        public int UsersId { get; set; }
        public User User { get; set; }

        public int BasesId { get; set; }
        public Base Base { get; set; }
    }
}

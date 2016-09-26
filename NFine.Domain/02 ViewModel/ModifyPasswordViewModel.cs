using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._02_ViewModel
{
   public class ModifyPasswordViewModel
    {
        public string UserAccount { get; set; }

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

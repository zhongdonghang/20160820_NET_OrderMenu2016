using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.MenuBiz
{
    public class T_ORDER_CHECKOUTEntity
    {
        public int OID { get; set; }
        public string OrderNo { get; set; }
        public decimal Price1 { get; set; }
        public decimal Price2 { get; set; }
        public string Dec { get; set; }
        public int OrgID { get; set; }
        public DateTime CreateOn { get; set; }
        public int CreateUserId { get; set; }
        public string CreateBy { get; set; }
    }
}

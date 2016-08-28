using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.MenuBiz
{
    public class T_ORDEREntity
    {
        public int OID { get; set; }
        public string OrderNo { get; set; }
        public DateTime CreateTime { get; set; }
        public string Seat { get; set; }
        public int PeopleNum { get; set; }
        public string MemberName { get; set; }
        public string Dec { get; set; }
        public int OrderState { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}

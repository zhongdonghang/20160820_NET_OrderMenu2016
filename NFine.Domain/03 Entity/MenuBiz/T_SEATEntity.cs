using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.MenuBiz
{
    public class T_SEATEntity
    {
        public int OID { get; set; }
        public int ParentID { get; set; }
        public string SeatNo { get; set; }
        public string SaatCategory { get; set; }
        public int PersonNum { get; set; }
        public int OrgID { get; set; }
        public string Desc { get; set; }
        public string Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.MenuBiz
{
    public class T_ORG_CATEGORYEntity
    {
        public int OID { get; set; }
        public int ParentID { get; set; }
        public string CategoryName { get; set; }
        public string Desc { get; set; }
    }
}

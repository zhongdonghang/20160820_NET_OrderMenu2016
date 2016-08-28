using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.MenuBiz
{
    public class T_PRODUCT_CATEORYEntity
    {
        public int OID { get; set; }
        public int ParentID { get; set; }
        public string Code { get; set; }
        public string EName { get; set; }
        public string CName { get; set; }
        public string ICONID { get; set; }
        public int AllowEdit { get; set; }
        public int OrgID { get; set; }
        public int DeletionStateCode { get; set; }
        public int Enabled { get; set; }
        public int SortCode { get; set; }
        public string Description { get; set; }
        public DateTime CreateOn { get; set; }
        public int CreateUserId { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedUserId { get; set; }
        public string ModifiedBy { get; set; }

    }
}

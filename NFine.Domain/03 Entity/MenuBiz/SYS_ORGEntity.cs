using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.MenuBiz
{
    public class SYS_ORGEntity
    {
        public int OID { get; set; }
        public int ParentId { get; set; }
        public int MasterImage { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string MemberNum { get; set; }
        public int SeatNum { get; set; }
        public string Desc { get; set; }
        public int OrgCategory { get; set; }
        public string Tel { get; set; }
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

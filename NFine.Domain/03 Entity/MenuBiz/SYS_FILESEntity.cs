using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.MenuBiz
{
    public class SYS_FILESEntity
    {
        public int OID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
        public string ImageUrl { get; set; }
        public int ReadCount { get; set; }
        public int OrgID { get; set; }
        public int Category { get; set; }
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

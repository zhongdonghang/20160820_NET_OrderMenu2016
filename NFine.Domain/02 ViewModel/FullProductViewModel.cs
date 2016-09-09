using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._02_ViewModel
{
    public class FullProductViewModel
    {
        public int OID { get; set; }
        public string Code { get; set; }
        public string CName { get; set; }
        public decimal Price1 { get; set; }
        public decimal Price2 { get; set; }
        public decimal Price3 { get; set; }
        public string PriceString { get; set; }
        public int PCategory { get; set; }
        public string PContent { get; set; }
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

        public string filePath { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.MenuBiz
{
    public class T_ORDER_INFOEntity
    {
        public int OID { get; set; }
        public string OrderNo { get; set; }
        public string ProductId { get; set; }
        public string ProductCname { get; set; }
        public double PNum { get; set; }
        public decimal Price1 { get; set; }
        public decimal Price2 { get; set; }
        public string MemberName { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.MenuBiz
{
    //
    //
    public class Report_DayEntity
    {
        public int OID { get; set; }
        public int 订单数量 { get; set; }
        public decimal 营业额 { get; set; }
        public decimal 最高单额 { get; set; }
        public decimal 最低单额 { get; set; }
        public decimal 平均单额 { get; set; }
        public int OrgID { get; set; }
        public DateTime CrDate { get; set; }
        public int 年 { get; set; }
        public int 月 { get; set; }
        public int 日 { get; set; }

        public string CrDateString
        {
            get {
                return CrDate.ToString("yyyy年MM月dd日");
            }
        }
    }
}

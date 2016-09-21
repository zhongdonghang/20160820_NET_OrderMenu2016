using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._02_ViewModel.Rpt
{
    /// <summary>
    /// 营业额
    /// </summary>
    public class RptSubDaysales
    {
        
        public int Day { get; set; }
        public decimal Total { get; set; }
    }


    /// <summary>
    /// 当月营业额趋势表
    /// </summary>
    public  class RptCurrentSalesLineViewModel
    {
        public int OrgId { get; set; }
        public int Month { get; set; }

        public List<RptSubDaysales> List { get; set; }
    }
}

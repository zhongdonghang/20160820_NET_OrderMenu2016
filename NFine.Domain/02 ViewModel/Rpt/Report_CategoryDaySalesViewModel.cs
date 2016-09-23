using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._02_ViewModel.Rpt
{

    public class Report_CategoryDaySalesItem
    {
        public int OID { get; set; }
        public int OrgID { get; set; }
        public DateTime CreateDate { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int SalesCount { get; set; }
        public decimal SaleTotal { get; set; }
    }

    /// <summary>
    /// 分类销售统计报表视图模型
    /// </summary>
   public class Report_CategoryDaySalesViewModel
    {
        public string Title { get; set; }
        public List<Report_CategoryDaySalesItem> RptList { get; set; }

        public string CategoriesString
        {
            get
            {
                string s = "";// categories: ['快捷套餐', '炒饭', '干捞类', '粥类', '加菜类'],
                foreach (var item in RptList)
                {
                    s += item.CategoryName ;
                    s += ",";
                }
                s = s.TrimEnd(',');
                return s;
            }
        }

        public string SalesCountString
        {
            get
            {
                string s = "";// [133, 156, 947, 408, 6]
                foreach (var item in RptList)
                {
                    s +=  item.SalesCount;
                    s += ",";
                }
                s = s.TrimEnd(',');
                return s;
            }
        }

        public string SaleTotalString
        {
            get
            {
                string s = "";
                foreach (var item in RptList)
                {
                    s += item.SaleTotal;
                    s += ",";
                }
                s = s.TrimEnd(',');
                return s;
            }
        }
    }
}

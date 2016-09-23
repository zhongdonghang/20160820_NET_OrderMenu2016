using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._02_ViewModel.Rpt
{
    public class Report_ProductStructureItem
    {
        public int OID { get; set; }
        public int OrgID { get; set; }
        public DateTime CreateDate { get; set; }
        public string CategoryName { get; set; }
        public int CategoryCount { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
    }

    /// <summary>
    /// 商品组成结构视图模型
    /// </summary>
    public class Report_ProductStructureViewModel
    {
        public string Title { get; set; }
        public List<Report_ProductStructureItem> RptList { get; set; }

        public string CategoriesString
        {
            get
            {
                string s = "";
                foreach (var item in RptList)
                {
                    s += item.CategoryName;
                    s += ",";
                }
                s = s.TrimEnd(',');
                return s;
            }
        }

        public string CategoryCountString
        {
            get
            {
                string s = "";// [133, 156, 947, 408, 6]
                foreach (var item in RptList)
                {
                    s += item.CategoryCount;
                    s += ",";
                }
                s = s.TrimEnd(',');
                return s;
            }
        }

        public string AvgPriceString
        {
            get
            {
                string s = "";// [133, 156, 947, 408, 6]
                foreach (var item in RptList)
                {
                    s += item.AvgPrice;
                    s += ",";
                }
                s = s.TrimEnd(',');
                return s;
            }
        }

        public string MaxPriceString
        {
            get
            {
                string s = "";// [133, 156, 947, 408, 6]
                foreach (var item in RptList)
                {
                    s += item.MaxPrice;
                    s += ",";
                }
                s = s.TrimEnd(',');
                return s;
            }
        }

        public string MinPriceString
        {
            get
            {
                string s = "";// [133, 156, 947, 408, 6]
                foreach (var item in RptList)
                {
                    s += item.MinPrice;
                    s += ",";
                }
                s = s.TrimEnd(',');
                return s;
            }
        }
    }
}

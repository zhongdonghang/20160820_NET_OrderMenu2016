using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._02_ViewModel.Rpt
{

    public class RptTopSalesProductItem
    {
        public string ProductName { get; set; }
        public int SaleCount { get; set; }
        public decimal Price { get; set; }
    }

    /// <summary>
    /// 商品销售排行榜统计报表
    /// </summary>
    public class RptTopSalesProductsViewModel
    {
        public string RptTopSalesName { get; set; }
        public string RptLastSalesName { get; set; }
        public string CreateDate { get; set; }

        public List<RptTopSalesProductItem> ProductsTopSales { get; set; }
        public List<RptTopSalesProductItem> ProductsLastSales { get; set; }


    }
}

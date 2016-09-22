using NFine.Code;
using NFine.Data.Extensions;
using NFine.Domain._02_ViewModel.Rpt;
using NFine.Domain._03_Entity.MenuBiz;
using NFine.Domain._04_IRepository.MenuBiz;
using NFine.Repository.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.MenuService
{
   public class SimpReportApp
    {

        private IReport_DayRepository service = new Report_DayRepository();
        private IReport_MonthRepository monthService = new Report_MonthRepository();

        private IT_ORDERRepository orderService = new T_ORDERRepository();
        private IT_ORDER_INFORepository orderInfoService = new T_ORDER_INFORepository();
        private IT_ORDER_CHECKOUTRepository checkOutInfoService = new T_ORDER_CHECKOUTRepository();

        /// <summary>
        /// 商品销售排行榜
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="OrgID"></param>
        /// <returns></returns>
        public RptTopSalesProductsViewModel GetRptTopSalesProductsViewModel(string beginDate, string endDate, int OrgID)
        {
            RptTopSalesProductsViewModel vm = new RptTopSalesProductsViewModel();
            string cacheKey = OrgID.ToString() + beginDate + endDate + "RptTopSalesProductsViewModel";
            vm = CacheFactory.Cache().GetCache<RptTopSalesProductsViewModel>(cacheKey);
            if (vm == null)
            {
                vm = new RptTopSalesProductsViewModel();
                string sql = "select  * from Report_ProductSaleDays where 销售日期>='" + beginDate + "' and 销售日期<='" + endDate + "'  and OrgID = 20 order by 销售商品数量 desc ";
                DataTable dt = DbHelper.QueryDataTable(sql);
                vm.RptTopSalesName = beginDate + "号到" + endDate + "号 Top 5 畅销商品排行";
                vm.RptLastSalesName = beginDate + "-" + endDate + "Top 5 滞销商品排行";
                vm.ProductsTopSales = new List<RptTopSalesProductItem>();
                vm.ProductsLastSales = new List<RptTopSalesProductItem>();

                for (int i = 0; i < 5; i++) //初始化为每个集合5个元素
                {
                    RptTopSalesProductItem it = new RptTopSalesProductItem();
                    it.Price = 0;
                    it.ProductName = "暂无";
                    it.SaleCount = 0;

                    vm.ProductsTopSales.Add(it);
                    vm.ProductsLastSales.Add(it);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 5) break;
                    vm.ProductsTopSales[i].ProductName = dt.Rows[i]["销售商品名称"].ToString();
                    vm.ProductsTopSales[i].SaleCount = int.Parse(dt.Rows[i]["销售商品数量"].ToString());
                    vm.ProductsTopSales[i].Price = decimal.Parse(dt.Rows[i]["商品单价"].ToString());
                }

                CacheFactory.Cache().WriteCache<RptTopSalesProductsViewModel>(vm, cacheKey);
            }
            return vm;
        }

        /// <summary>
        /// 获取指定月份的每日营业额
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public RptCurrentSalesLineViewModel GetRptCurrentSalesLineViewModel(int OrgId,int Month)
        {
            RptCurrentSalesLineViewModel vm = new RptCurrentSalesLineViewModel();
            string cacheKey = OrgId.ToString() + Month.ToString() + "RptCurrentSalesLineViewModel";
            vm = CacheFactory.Cache().GetCache<RptCurrentSalesLineViewModel>(cacheKey);
            if (vm == null)
            {
              
                vm = new RptCurrentSalesLineViewModel();
                vm.Month = Month;
                string sql = "select  营业额,日 from Report_Day where 月 = " + Month + " and orgid = " + OrgId + "";
                DataTable dt = DbHelper.QueryDataTable(sql);

                int monthDays = 31;
                vm.List = new List<RptSubDaysales>();
                for (int i = 1; i <= monthDays; i++)
                {
                    RptSubDaysales sd = new RptSubDaysales();
                    sd.Day = i;
                    sd.Total = 0;
                    vm.List.Add(sd);
                }
                foreach (RptSubDaysales item in vm.List)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[1].ToString() == item.Day.ToString())
                        {
                            item.Total = decimal.Parse(row[0].ToString());
                        }
                    }
                }
                CacheFactory.Cache().WriteCache<RptCurrentSalesLineViewModel>(vm, cacheKey);
            }

            return vm;
        }

        /// <summary>
        /// 查询获得月报表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public List<Report_MonthEntity> GetMonthList(Pagination pagination, int OrgId)
        {
            var expression = ExtLinq.True<Report_MonthEntity>();
            expression = expression.And(t => t.OrgID == OrgId);
            return monthService.FindList(expression, pagination);
        }



        /// <summary>
        ///查询日报表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public List<Report_DayEntity> GetList(Pagination pagination, int OrgId)
        {
            var expression = ExtLinq.True<Report_DayEntity>();
            expression = expression.And(t => t.OrgID == OrgId);
            return service.FindList(expression, pagination);
        }

       
    }
}

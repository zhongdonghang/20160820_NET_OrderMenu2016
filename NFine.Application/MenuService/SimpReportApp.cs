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

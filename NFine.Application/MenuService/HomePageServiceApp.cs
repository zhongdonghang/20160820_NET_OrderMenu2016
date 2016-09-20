using NFine.Code;
using NFine.Data.Extensions;
using NFine.Domain._02_ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.MenuService
{
    public class HomePageServiceApp
    {
        public HomeDefaultViewModel GetHomePageVM()
        {
            HomeDefaultViewModel vm = new HomeDefaultViewModel();
            int OrgId = OperatorProvider.Provider.GetCurrent().OrgId;
            vm = CacheFactory.Cache().GetCache<HomeDefaultViewModel>(OrgId + "DefaultViewModel");
            if (vm == null)
            {
                string sql = "select count(OID) as 当前在线订单数  from T_ORDER where OrderState = 1 and OrgID = " + OrgId + " " +
                    " select count(OID) as 当天所有订单数 from T_ORDER where (OrderState = 1 or OrderState = 2)  " +
                    " and OrgID = " + OrgId + " " +
                    " and CONVERT(varchar(100), GETDATE(), 23)= CONVERT(varchar(100), CreateTime, 23) " +
                    " select sum(Price1)当天总销售额 from T_ORDER_INFO where OrderNo in  " +
                    " (select OrderNo from T_ORDER where (OrderState = 1 or OrderState = 2)  " +
                    " and OrgID = " + OrgId + " and CONVERT(varchar(100), GETDATE(), 23)= CONVERT(varchar(100), CreateTime, 23)) " +
                    " select sum(Price2) as 当天已收金额 from T_ORDER_CHECKOUT " +
                    " where " +
                    " OrgID = " + OrgId + " and " +
                    " CONVERT(varchar(100), GETDATE(), 23) = CONVERT(varchar(100), CreateOn, 23) " +
                    " declare @orderSum money " +
                    " select @orderSum = sum(Price1) from T_ORDER_INFO where OrderNo in  " +
                    " (select OrderNo from T_ORDER where (OrderState = 1 or OrderState = 2)  " +
                    " and OrgID = " + OrgId + " and " +
                    " CONVERT(varchar(100), GETDATE(), 23) = CONVERT(varchar(100), CreateTime, 23)) " +
                    " declare @orderNums int " +
                    " select @orderNums = count(OID) from T_ORDER where (OrderState = 1 or OrderState = 2)  " +
                    " and OrgID = " + OrgId + " " +
                    " and CONVERT(varchar(100), GETDATE(), 23)= CONVERT(varchar(100), CreateTime, 23) " +
                    " select @orderSum/ @orderNums as 当天平均单额";
                DataSet ds = DbHelper.QueryDataSet(sql);
                vm = new HomeDefaultViewModel();
                vm.ObjHomePageTopViewModel = new HomePageTopViewModel();

                vm.ObjHomePageTopViewModel.当前在线订单数 = ds.Tables[0].Rows[0][0].ToString();
                vm.ObjHomePageTopViewModel.当天订单总数 = ds.Tables[1].Rows[0][0].ToString();
                vm.ObjHomePageTopViewModel.当天销售总额 = string.IsNullOrEmpty(ds.Tables[2].Rows[0][0].ToString()) ? "0" : ds.Tables[2].Rows[0][0].ToString();
                vm.ObjHomePageTopViewModel.当天已收金额 = string.IsNullOrEmpty(ds.Tables[3].Rows[0][0].ToString()) ? "0" : ds.Tables[3].Rows[0][0].ToString();
                vm.ObjHomePageTopViewModel.当天平均单额 = string.IsNullOrEmpty(ds.Tables[4].Rows[0][0].ToString()) ? "0" : ds.Tables[4].Rows[0][0].ToString();


                sql = "select count(OID) from T_ORDER where "+
                    " CONVERT(varchar(100), GETDATE(), 23) = CONVERT(varchar(100), CreateTime, 23) "+
                    " and (datename(Hour, CreateTime) >= 0 and datename(Hour, CreateTime) < 2) " +
                    " and (OrderState = 1 or OrderState = 2)   and OrgID = " + OrgId + " " +
                    " select count(OID)from T_ORDER where " +
                    " CONVERT(varchar(100), GETDATE(), 23) = CONVERT(varchar(100), CreateTime, 23) " +
                    " and (datename(Hour, CreateTime) >= 2 and datename(Hour, CreateTime) < 4) " +
                    " and (OrderState = 1 or OrderState = 2)  and OrgID = " + OrgId + " " +
                    " select count(OID) from T_ORDER where " +
                    " CONVERT(varchar(100), GETDATE(), 23) = CONVERT(varchar(100), CreateTime, 23) " +
                    " and (datename(Hour, CreateTime) >= 4 and datename(Hour, CreateTime) < 6) " +
                    " and (OrderState = 1 or OrderState = 2)  and OrgID = " + OrgId + " " +
                    " select count(OID) from T_ORDER where " +
                    " CONVERT(varchar(100), GETDATE(), 23) = CONVERT(varchar(100), CreateTime, 23) " +
                    " and (datename(Hour, CreateTime) >= 6 and datename(Hour, CreateTime) < 8) " +
                    " and (OrderState = 1 or OrderState = 2)  and OrgID = " + OrgId + " " +
                    " select count(OID) from T_ORDER where " +
                    " CONVERT(varchar(100), GETDATE(), 23) = CONVERT(varchar(100), CreateTime, 23) " +
                    " and (datename(Hour, CreateTime) >= 8 and datename(Hour, CreateTime) < 10) " +
                    " and (OrderState = 1 or OrderState = 2)  and OrgID = " + OrgId + " " +
                    " select count(OID) from T_ORDER where " +
                    " CONVERT(varchar(100), GETDATE(), 23) = CONVERT(varchar(100), CreateTime, 23) " +
                    " and (datename(Hour, CreateTime) >= 10 and datename(Hour, CreateTime) < 12) " +
                    " and (OrderState = 1 or OrderState = 2)  and OrgID = " + OrgId + " " +
                    " select count(OID) from T_ORDER where " +
                    " CONVERT(varchar(100), GETDATE(), 23) = CONVERT(varchar(100), CreateTime, 23) " +
                    " and (datename(Hour, CreateTime) >= 12 and datename(Hour, CreateTime) < 14) " +
                    " and (OrderState = 1 or OrderState = 2)  and OrgID = " + OrgId + "  " +
                    " select count(OID) from T_ORDER where " +
                    " CONVERT(varchar(100), GETDATE(), 23) = CONVERT(varchar(100), CreateTime, 23) " +
                    " and (datename(Hour, CreateTime) >= 14 and datename(Hour, CreateTime) < 16) " +
                    " and (OrderState = 1 or OrderState = 2)  and OrgID = " + OrgId + "  " +
                    " select count(OID) from T_ORDER where " +
                    " CONVERT(varchar(100), GETDATE(), 23) = CONVERT(varchar(100), CreateTime, 23) " +
                    " and (datename(Hour, CreateTime) >= 16 and datename(Hour, CreateTime) < 18) " +
                    " and (OrderState = 1 or OrderState = 2) and OrgID = " + OrgId + "  " +
                    " select count(OID) from T_ORDER where " +
                    " CONVERT(varchar(100), GETDATE(), 23) = CONVERT(varchar(100), CreateTime, 23) " +
                    " and (datename(Hour, CreateTime) >= 18 and datename(Hour, CreateTime) < 20) " +
                    " and (OrderState = 1 or OrderState = 2)  and OrgID = " + OrgId + " " +
                    " select count(OID) from T_ORDER where " +
                    " CONVERT(varchar(100), GETDATE(), 23) = CONVERT(varchar(100), CreateTime, 23) " +
                    " and (datename(Hour, CreateTime) >= 20 and datename(Hour, CreateTime) < 22) " +
                    " and (OrderState = 1 or OrderState = 2) and OrgID = " + OrgId + " " +
                    " select count(OID) from T_ORDER where   " +
                    " CONVERT(varchar(100), GETDATE(), 23) = CONVERT(varchar(100), CreateTime, 23) " +
                    " and (datename(Hour, CreateTime) >= 22 and datename(Hour, CreateTime) < 24) " +
                    " and (OrderState = 1 or OrderState = 2)  and OrgID = " + OrgId + " ";

                ds.Tables.Clear();
                ds = DbHelper.QueryDataSet(sql);
                vm.ObjHomePageTimesOrderCountViewModel = new HomePageTimesOrderCountViewModel();
                vm.ObjHomePageTimesOrderCountViewModel.Hours2 = ds.Tables[0].Rows[0][0].ToString();
                vm.ObjHomePageTimesOrderCountViewModel.Hours4 = ds.Tables[1].Rows[0][0].ToString();
                vm.ObjHomePageTimesOrderCountViewModel.Hours6 = ds.Tables[2].Rows[0][0].ToString();
                vm.ObjHomePageTimesOrderCountViewModel.Hours8 = ds.Tables[3].Rows[0][0].ToString();
                vm.ObjHomePageTimesOrderCountViewModel.Hours10 = ds.Tables[4].Rows[0][0].ToString();
                vm.ObjHomePageTimesOrderCountViewModel.Hours12 = ds.Tables[5].Rows[0][0].ToString();
                vm.ObjHomePageTimesOrderCountViewModel.Hours14 = ds.Tables[6].Rows[0][0].ToString();
                vm.ObjHomePageTimesOrderCountViewModel.Hours16 = ds.Tables[7].Rows[0][0].ToString();
                vm.ObjHomePageTimesOrderCountViewModel.Hours18 = ds.Tables[8].Rows[0][0].ToString();
                vm.ObjHomePageTimesOrderCountViewModel.Hours20 = ds.Tables[9].Rows[0][0].ToString();
                vm.ObjHomePageTimesOrderCountViewModel.Hours22 = ds.Tables[10].Rows[0][0].ToString();
                vm.ObjHomePageTimesOrderCountViewModel.Hours0 = ds.Tables[11].Rows[0][0].ToString();

                sql = "select ProductCname, sum(PNum) as 数量 from [dbo].[T_ORDER_INFO] where "+
                    " OrderNo like '"+OrgId+"%'  and "+
                    " CONVERT(varchar(100), GETDATE(), 23) = CONVERT(varchar(100), CreateOn, 23) "+
                    " group by ProductCname order by sum(PNum)desc";
                ds.Tables.Clear();
                ds = DbHelper.QueryDataSet(sql);
                vm.ObjHomePageSalesTop5ProductViewModel = new HomePageSalesTop5ProductViewModel();
                if (ds.Tables[0].Rows.Count>0)
                {
                    vm.ObjHomePageSalesTop5ProductViewModel.Top1ProductName = ds.Tables[0].Rows[0][0].ToString();
                    vm.ObjHomePageSalesTop5ProductViewModel.Top1ProductCount = ds.Tables[0].Rows[0][1].ToString();
                }
                else
                {
                    vm.ObjHomePageSalesTop5ProductViewModel.Top1ProductName = "暂无";
                    vm.ObjHomePageSalesTop5ProductViewModel.Top1ProductCount = "0";
                }

                if (ds.Tables[0].Rows.Count > 1)
                {
                    vm.ObjHomePageSalesTop5ProductViewModel.Top2ProductName = ds.Tables[0].Rows[1][0].ToString();
                    vm.ObjHomePageSalesTop5ProductViewModel.Top2ProductCount = ds.Tables[0].Rows[1][1].ToString();
                }
                else
                {
                    vm.ObjHomePageSalesTop5ProductViewModel.Top2ProductName = "暂无";
                    vm.ObjHomePageSalesTop5ProductViewModel.Top2ProductCount = "0";
                }


                if (ds.Tables[0].Rows.Count > 2)
                {
                    vm.ObjHomePageSalesTop5ProductViewModel.Top3ProductName = ds.Tables[0].Rows[2][0].ToString();
                    vm.ObjHomePageSalesTop5ProductViewModel.Top3ProductCount = ds.Tables[0].Rows[2][1].ToString();
                }
                else
                {
                    vm.ObjHomePageSalesTop5ProductViewModel.Top3ProductName = "暂无";
                    vm.ObjHomePageSalesTop5ProductViewModel.Top3ProductCount = "0";
                }

                if (ds.Tables[0].Rows.Count > 3)
                {
                    vm.ObjHomePageSalesTop5ProductViewModel.Top4ProductName = ds.Tables[0].Rows[3][0].ToString();
                    vm.ObjHomePageSalesTop5ProductViewModel.Top4ProductCount = ds.Tables[0].Rows[3][1].ToString();
                }
                else
                {
                    vm.ObjHomePageSalesTop5ProductViewModel.Top4ProductName = "暂无";
                    vm.ObjHomePageSalesTop5ProductViewModel.Top4ProductCount = "0";
                }

                if (ds.Tables[0].Rows.Count > 4)
                {
                    vm.ObjHomePageSalesTop5ProductViewModel.Top5ProductName = ds.Tables[0].Rows[4][0].ToString();
                    vm.ObjHomePageSalesTop5ProductViewModel.Top5ProductCount = ds.Tables[0].Rows[4][1].ToString();
                }
                else
                {
                    vm.ObjHomePageSalesTop5ProductViewModel.Top5ProductName = "暂无";
                    vm.ObjHomePageSalesTop5ProductViewModel.Top5ProductCount = "0";
                }

                CacheFactory.Cache().WriteCache<HomeDefaultViewModel>(vm, OrgId + "DefaultViewModel");
            }
            return vm;

        }
    }
}

using NFine.Application.MenuService;
using NFine.Code;
using NFine.Domain._02_ViewModel.Rpt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.MenuSys.Controllers
{
    /// <summary>
    /// 分类销售统计报表
    /// </summary>
    public class Report_CategoryDaySalesController : ControllerBase
    {
        //
        // GET: /MenuSys/Report_CategoryDaySales/
        private SimpReportApp objSimpReportApp = new SimpReportApp();
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult CreateRptLastPage()
        {
            ViewResult vr = new ViewResult();
            vr.ViewName = "Report_CategoryDaySalesPage";
            string beginDate = "";
            string endDate = "";
            string namestr = "";
            if (Request["m"] == null)
            {
                beginDate = DateTime.Now.ToString("yyyy-MM-dd");
                endDate = DateTime.Now.ToString("yyyy-MM-dd");
                namestr = "昨天";
            }
            else
            {
                string reqM = Request["m"].ToString();
               
                if (reqM == "0") //当天（其实数据库取的是前一天）
                {
                    beginDate = DateTime.Now.ToString("yyyy-MM-dd");
                    endDate = DateTime.Now.ToString("yyyy-MM-dd");
                    namestr = "昨天";
                }
                else if (reqM == "1") //最近7天
                {
                    beginDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                    endDate = DateTime.Now.ToString("yyyy-MM-dd");
                    namestr = "最近7天";
                }
                else if (reqM == "2") //最近一个月
                {
                    beginDate = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                    endDate = DateTime.Now.ToString("yyyy-MM-dd");
                    namestr = "最近一个月";
                }
            }
            int OrgID = OperatorProvider.Provider.GetCurrent().OrgId;
            Report_CategoryDaySalesViewModel vm = objSimpReportApp.GetReport_CategoryDaySalesViewModel(namestr,beginDate, endDate, OrgID);
            ViewDataDictionary dic = new ViewDataDictionary(vm);
            vr.ViewData = dic;
            return vr;
        }

    }
}

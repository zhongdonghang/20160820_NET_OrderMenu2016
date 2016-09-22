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
    /// 商品销售排行报表控制器
    /// </summary>
    public class RptTopSalesProductsController : ControllerBase
    {
        //
        // GET: /MenuSys/RptTopSalesProducts/
        private SimpReportApp objSimpReportApp = new SimpReportApp();

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult CreateRptPage()
        {
            ViewResult vr = new ViewResult();
            vr.ViewName = "RptPage";
            string beginDate = "";
            string endDate = "";
            if (Request["m"] == null)
            {
                beginDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                endDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }
            else
            {
                string reqM = Request["m"].ToString();
                if (reqM == "0") //前一天
                {
                    beginDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    endDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                else if (reqM == "1") //最近7天
                {
                    beginDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                    endDate = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else if (reqM == "2") //最近一个月
                {
                    beginDate = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                    endDate = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            int OrgID = OperatorProvider.Provider.GetCurrent().OrgId;
            RptTopSalesProductsViewModel vm = objSimpReportApp.GetRptTopSalesProductsViewModel(beginDate, endDate, OrgID);
            ViewDataDictionary dic = new ViewDataDictionary(vm);
            vr.ViewData = dic;
            return vr;
        }


    }
}

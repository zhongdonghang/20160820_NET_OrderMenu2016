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
    public class MonthSalesLineController : Controller
    {
        //
        // GET: /MenuSys/MonthSalesLine/
        private SimpReportApp objSimpReportApp = new SimpReportApp();

        public ViewResult Index()
        {
            int month = Request["m"] == null ? DateTime.Now.Month : int.Parse(Request["m"].ToString());
            int orgId = OperatorProvider.Provider.GetCurrent().OrgId;
            RptCurrentSalesLineViewModel vm = objSimpReportApp.GetRptCurrentSalesLineViewModel(orgId, month);
            ViewResult vr = new ViewResult();
            vr.ViewName = "Index";
            ViewDataDictionary dic = new ViewDataDictionary(vm);
            vr.ViewData = dic;
            return vr;
        }

    }
}

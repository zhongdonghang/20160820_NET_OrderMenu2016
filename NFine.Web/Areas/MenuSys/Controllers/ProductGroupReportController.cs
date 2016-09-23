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
    public class ProductGroupReportController : ControllerBase
    {
        //
        // GET: /MenuSys/ProductGroupReport/
        private SimpReportApp objSimpReportApp = new SimpReportApp();
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult GetRptPage()
        {
            ViewResult vr = new ViewResult();
            vr.ViewName = "Report_CategoryDaySalesPage";
            int OrgID = OperatorProvider.Provider.GetCurrent().OrgId;
            Report_ProductStructureViewModel vm = objSimpReportApp.GetReport_ProductStructureViewModel(OrgID);
            ViewDataDictionary dic = new ViewDataDictionary(vm);
            vr.ViewData = dic;
            return vr;
        }

    }
}

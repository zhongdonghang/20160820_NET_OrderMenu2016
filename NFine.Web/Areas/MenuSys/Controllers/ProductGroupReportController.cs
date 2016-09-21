using NFine.Application.MenuService;
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

    }
}

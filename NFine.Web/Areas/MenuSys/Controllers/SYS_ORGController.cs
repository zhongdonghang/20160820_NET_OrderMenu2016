using NFine.Application.MenuService;
using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.MenuSys.Controllers
{
    public class SYS_ORGController : ControllerBase
    {
        private SYS_ORGApp sYS_ORGApp = new SYS_ORGApp();

        //
        // GET: /MenuSys/SYS_ORG/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = sYS_ORGApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

    }
}

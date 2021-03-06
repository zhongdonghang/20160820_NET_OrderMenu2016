﻿using NFine.Application.MenuService;
using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.MenuSys.Controllers
{
    public class DayReportController : ControllerBase
    {
        //
        // GET: /MenuSys/DayReport/
        private SimpReportApp objSimpReportApp = new SimpReportApp();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = objSimpReportApp.GetList(pagination, OperatorProvider.Provider.GetCurrent().OrgId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        //[HttpPost]
        //[HandlerAuthorize]
        //[HandlerAjaxOnly]
        //public ActionResult ReCreateAllReport()
        //{
        //    objSimpReportApp.CreateDayReport(OperatorProvider.Provider.GetCurrent().OrgId);
        //    return Success("全部日报表生成成功。");
        //}
    }
}

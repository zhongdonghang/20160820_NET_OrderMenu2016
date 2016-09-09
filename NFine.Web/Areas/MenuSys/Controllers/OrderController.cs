using NFine.Application.MenuService;
using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.MenuSys.Controllers
{
    public class OrderController : ControllerBase
    {
        //
        // GET: /MenuSys/Order/

        private OrderApp objOrderApp = new OrderApp();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取列表显示在首页
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = objOrderApp.GetList(pagination, keyword, OperatorProvider.Provider.GetCurrent().OrgId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
    }
}

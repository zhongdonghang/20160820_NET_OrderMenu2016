using NFine.Application.MenuService;
using NFine.Code;
using NFine.Domain._02_ViewModel;
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
        /// 实际支付
        /// </summary>
        /// <returns></returns>
        public ActionResult PayMoney()
        {
            string OrderNo =  Request.Form["OrderNo"];
            string Price1 = Request.Form["Price1"];
            string Price2 = Request.Form["Price2"];
            string Dec = Request.Form["Dec"];
            objOrderApp.Pay(OrderNo, Price1, Price2, Dec);
            return Success("支付成功");
        }


        public ActionResult Pay(string keyValue)
        {
            FullOrder order = objOrderApp.GetOneByKey(keyValue);
            ViewResult vr = new ViewResult();
            vr.ViewName = "Pay1";
            ViewDataDictionary dic = new ViewDataDictionary(order);
            vr.ViewData = dic;
            return vr;
           // return Content(order.ToJson());
        }

        /// <summary>
        /// 查询未结账订单
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

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetAllGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = objOrderApp.GetAllList(pagination, keyword, OperatorProvider.Provider.GetCurrent().OrgId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            objOrderApp.DeleteForm(keyValue);
            return Success("作废成功。");
        }
    }
}

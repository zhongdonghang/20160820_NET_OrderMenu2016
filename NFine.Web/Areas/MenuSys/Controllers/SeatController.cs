using NFine.Application.MenuService;
using NFine.Code;
using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.MenuSys.Controllers
{
    /// <summary>
    /// 台位控制器
    /// </summary>
    public class SeatController : ControllerBase
    {
        //
        // GET: /MenuSys/Seat/

        private SeatApp objSeatApp = new SeatApp();

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
                rows = objSeatApp.GetList(pagination, keyword, OperatorProvider.Provider.GetCurrent().OrgId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = objSeatApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            objSeatApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(T_SEATEntity objT_SEATEntity, string keyValue)
        {
            objT_SEATEntity.OrgID = OperatorProvider.Provider.GetCurrent().OrgId;
            objSeatApp.SubmitForm(objT_SEATEntity, keyValue);
            return Success("操作成功。");
        }

    }
}

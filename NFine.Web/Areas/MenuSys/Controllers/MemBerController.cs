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
    /// 服务员控制器
    /// </summary>
    public class MemBerController : ControllerBase
    {
        //
        // GET: /MenuSys/MemBer/

        private MembersApp objMembersApp = new MembersApp();

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
                rows = objMembersApp.GetList(pagination, keyword, OperatorProvider.Provider.GetCurrent().OrgId),
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
            var data = objMembersApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            objMembersApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(T_MEMBERSEntity objT_MEMBERSEntity, string keyValue)
        {
            objT_MEMBERSEntity.OrgID = OperatorProvider.Provider.GetCurrent().OrgId;
            objMembersApp.SubmitForm(objT_MEMBERSEntity, keyValue);
            return Success("操作成功。");
        }

    }
}

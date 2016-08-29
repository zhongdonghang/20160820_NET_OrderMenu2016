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
    /// 商品类别控制器
    /// </summary>
    public class CategoryController : ControllerBase
    {
        //
        // GET: /MenuSys/Category/
        private CategoryApp objCategoryApp = new CategoryApp();

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
                rows = objCategoryApp.GetList(pagination, keyword, OperatorProvider.Provider.GetCurrent().OrgId),
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
            var data = objCategoryApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(T_PRODUCT_CATEORYEntity objT_PRODUCT_CATEORYEntity,  string keyValue)
        {
            objT_PRODUCT_CATEORYEntity.OrgID = OperatorProvider.Provider.GetCurrent().OrgId;
            objCategoryApp.SubmitForm(objT_PRODUCT_CATEORYEntity, keyValue);
            return Success("操作成功。");
        }

    }
}

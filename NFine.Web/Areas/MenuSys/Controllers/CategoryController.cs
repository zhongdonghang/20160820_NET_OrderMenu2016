﻿using NFine.Application.MenuService;
using NFine.Code;
using NFine.Domain._03_Entity.MenuBiz;
using NFine.Domain.Entity.SystemManage;
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

        /// <summary>
        /// 获取类别列表显示在首页
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
                rows = objCategoryApp.GetList(pagination, keyword, OperatorProvider.Provider.GetCurrent().OrgId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取类别列表用于绑定到下拉框
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetAllGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = objCategoryApp.GetList(keyword, OperatorProvider.Provider.GetCurrent().OrgId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };

            List<object> list = new List<object>();
            foreach (var item in data.rows)
            {
                list.Add(new { id = item.OID, text = item.CName });
            }
            return Content(list.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = objCategoryApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            objCategoryApp.DeleteForm(keyValue);
            return Success("删除成功。");
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

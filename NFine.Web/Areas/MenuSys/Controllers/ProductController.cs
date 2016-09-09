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
    /// 商品控制器
    /// </summary>
    public class ProductController : ControllerBase
    {
        //
        // GET: /MenuSys/Product/

        ProductApp objProductApp = new ProductApp();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadImage(string keyValue)
        {
            return View("SettingImageForm");
        }


        public ActionResult UploadProgressImage(HttpPostedFileBase file)
        {
            string productOID = Request.Params["txtOID"].ToString();
            if (string.IsNullOrEmpty(productOID))
            {
                return Error("缺失的商品编号，请重新打开上传");
            }
            else if (file == null)
            {
                return Error("请选择一张商品图片");
            }
            else if (file.ContentType != "image/jpeg" && file.ContentType != "image/png")
            {
                return Error("只能上传jpg或者png格式的图片");
            }
            else
            {
                SYS_FILESEntity fileEntity = new SYS_FILESEntity();
                string name = Common.CreateNo();
                fileEntity.FileSize = file.ContentLength;
                if (file.ContentType == "image/jpeg")
                {
                    fileEntity.FileName = name + ".jpg";
                    fileEntity.FilePath = "/uploadFiles/" + fileEntity.FileName + ".jpg";
                }
                else
                {
                    fileEntity.FileName = name + ".png";
                    fileEntity.FilePath = "/uploadFiles/" + fileEntity.FileName + ".png";
                }
                file.SaveAs(Server.MapPath("~/uploadFiles/"+ fileEntity.FileName));
                objProductApp.SettingImageForProduct(fileEntity, productOID);
            }
            return Success("上传成功");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            objProductApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = objProductApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(T_PRODUCTEntity objT_PRODUCTEntity, string keyValue)
        {
            objT_PRODUCTEntity.OrgID = OperatorProvider.Provider.GetCurrent().OrgId;
            objProductApp.SubmitForm(objT_PRODUCTEntity,keyValue);
            return Success("操作成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = objProductApp.GetList(pagination, keyword, OperatorProvider.Provider.GetCurrent().OrgId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

    }
}

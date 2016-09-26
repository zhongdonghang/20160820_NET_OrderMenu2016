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


        public ActionResult UploadProgressImage(HttpPostedFileBase Filedata)// HttpPostedFileBase Filedata
        {

            SYS_FILESEntity fileEntity = new SYS_FILESEntity();
            string name = Common.CreateNo();
            fileEntity.FileSize = Filedata.ContentLength;
            if (Filedata.ContentType == "image/jpeg")
            {
                fileEntity.FileName = name + ".jpg";
                fileEntity.FilePath = "/uploadFiles/" + fileEntity.FileName + ".jpg";
            }
            else
            {
                fileEntity.FileName = name + ".png";
                fileEntity.FilePath = "/uploadFiles/" + fileEntity.FileName + ".png";
            }
            Filedata.SaveAs(Server.MapPath("~/uploadFiles/" + fileEntity.FileName));
            //    objProductApp.SettingImageForProduct(fileEntity, productOID);

            return Success(fileEntity.FileName);
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

        //[HttpPost]
        //[HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitForm(T_PRODUCTEntity objT_PRODUCTEntity, string keyValue)
        {
            string imagePath = Request["pImagePath"].ToString();
            if (imagePath == "empty")
            {
                objT_PRODUCTEntity.OrgID = OperatorProvider.Provider.GetCurrent().OrgId;
                objProductApp.SubmitForm(objT_PRODUCTEntity, keyValue, null);
            }
            else
            {
                string imageName = imagePath.Substring(imagePath.LastIndexOf("/") + 1);
                SYS_FILESEntity fileEntity = new SYS_FILESEntity();
                fileEntity.FileName = imageName;
                fileEntity.FilePath = "/uploadFiles/" + imageName; //201607241050101242.jpg
                fileEntity.FileSize = 0;
                fileEntity.ReadCount = 0;
                fileEntity.OrgID = OperatorProvider.Provider.GetCurrent().OrgId;
                fileEntity.Category = 0;
                fileEntity.DeletionStateCode = 0;
                fileEntity.Enabled = 0;
                fileEntity.SortCode = 0;
                fileEntity.Description = "--";
                fileEntity.CreateOn = DateTime.Now;
                fileEntity.CreateUserId = 0;
                fileEntity.CreateBy = OperatorProvider.Provider.GetCurrent().UserName;
                fileEntity.ModifiedOn = DateTime.Now;
                fileEntity.ModifiedUserId = 0;
                fileEntity.ModifiedBy = OperatorProvider.Provider.GetCurrent().UserName;
                objT_PRODUCTEntity.OrgID = OperatorProvider.Provider.GetCurrent().OrgId;
                objProductApp.SubmitForm(objT_PRODUCTEntity, keyValue, fileEntity);
            }
            return Success("操作成功");
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

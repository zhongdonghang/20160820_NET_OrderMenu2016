using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.MenuSys.Controllers
{
    /// <summary>
    /// 个人设置
    /// </summary>
    public class BossSettingController : ControllerBase
    {
        //
        // GET: /MenuSys/BossSetting/GetSettingInfo
        private UserApp userApp = new UserApp();
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //[HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitForm(UserEntity userEntity)
        {
            string F_Id = Request["F_Id"].ToString();
            userEntity.F_Id = F_Id;
            userApp.Modify(userEntity);
            return Success("操作成功。");
        }

        public ViewResult GetSettingInfo()
        {

            UserEntity user = userApp.GetForm(OperatorProvider.Provider.GetCurrent().UserId);
            ViewResult vr = new ViewResult();
            vr.ViewName = "Setting";
            ViewDataDictionary dic = new ViewDataDictionary(user);
            vr.ViewData = dic;
            return vr;
        }

    }
}

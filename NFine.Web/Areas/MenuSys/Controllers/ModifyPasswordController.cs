using NFine.Application.MenuService;
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain._02_ViewModel;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.MenuSys.Controllers
{
    public class ModifyPasswordController : ControllerBase
    {
        //
        // GET: /MenuSys/ModifyPassword/
        UserApp userApp = new UserApp();
        UserLogOnApp objUserLogOnApp = new UserLogOnApp();

        UserInfoApp objUserInfoApp = new UserInfoApp();

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult GetSettingInfo()
        {
            ModifyPasswordViewModel vm = objUserInfoApp.GetByUserID(OperatorProvider.Provider.GetCurrent().UserId);
            ViewResult vr = new ViewResult();
            vr.ViewName = "Setting";
            ViewDataDictionary dic = new ViewDataDictionary(vm);
            vr.ViewData = dic;
            return vr;
        }

        public ActionResult SubmitForm(ModifyPasswordViewModel objModifyPasswordViewModel)
        {
            bool isTrue = objUserInfoApp.ModifyPassword(objModifyPasswordViewModel.OldPassword, objModifyPasswordViewModel.NewPassword);
            if (isTrue)
            {
                return Success("操作成功。");
            }
            else
            {
                return Error("输入的旧密码不对。");
            }
        }


    }
}

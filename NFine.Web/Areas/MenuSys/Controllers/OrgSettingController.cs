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
    public class OrgSettingController : ControllerBase
    {
        //
        // GET: /MenuSys/OrgSetting/GetSettingInfo
        private OrganizeApp ObjOrganizeApp = new OrganizeApp();

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult GetSettingInfo()
        {
            OrganizeEntity Org = ObjOrganizeApp.GetByOrgNo(OperatorProvider.Provider.GetCurrent().OrgId);
            ViewResult vr = new ViewResult();
            vr.ViewName = "Setting";
            ViewDataDictionary dic = new ViewDataDictionary(Org);
            vr.ViewData = dic;
            return vr;
        }

        public ActionResult SubmitForm(OrganizeEntity organizeEntity)
        {
            string F_Id = Request["F_Id"].ToString();
            organizeEntity.F_Id = F_Id;
            ObjOrganizeApp.Modify(organizeEntity);
            return Success("操作成功。");
        }

    }
}

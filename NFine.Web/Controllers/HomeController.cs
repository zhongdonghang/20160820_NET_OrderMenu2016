/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Application.MenuService;
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain._02_ViewModel;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace NFine.Web.Controllers
{
    [HandlerLogin]
    public class HomeController : Controller
    {
        HomePageServiceApp app = new HomePageServiceApp();

        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public ViewResult Default()
        {
            HomeDefaultViewModel vm = app.GetHomePageVM();

           // FullOrder order = objOrderApp.GetOneByKey(Request.Params["OID"]);
            ViewResult vr = new ViewResult();
            vr.ViewName = "Default";
            ViewDataDictionary dic = new ViewDataDictionary(vm);
            vr.ViewData = dic;

            return vr;
        }



        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
    }
}

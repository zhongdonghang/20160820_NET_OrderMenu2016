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

        public ActionResult Index()
        {
            return View();
        }

    }
}

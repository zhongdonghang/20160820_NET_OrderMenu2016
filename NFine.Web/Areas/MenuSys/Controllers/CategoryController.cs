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

        public ActionResult Index()
        {
            return View();
        }

    }
}

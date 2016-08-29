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

        public ActionResult Index()
        {
            return View();
        }

    }
}

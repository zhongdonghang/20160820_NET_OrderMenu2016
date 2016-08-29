using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.MenuSys.Controllers
{
    /// <summary>
    /// 台位控制器
    /// </summary>
    public class SeatController : ControllerBase
    {
        //
        // GET: /MenuSys/Seat/

        public ActionResult Index()
        {
            return View();
        }

    }
}

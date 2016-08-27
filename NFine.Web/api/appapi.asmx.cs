using NFine.Application.MenuService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace NFine.Web.api
{
    /// <summary>
    /// appapi 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://nnbetter.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class appapi : System.Web.Services.WebService
    {
        [WebMethod]
        public void GetOrgSimpleList(string _pageIndex,string _pageSize,string _cname)
        {
            HttpContext.Current.Response.Write(new SYS_ORGApp().GetOrgListForJson(_pageIndex, _pageSize, _cname));
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}

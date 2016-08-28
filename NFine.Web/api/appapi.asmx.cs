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
        
        [WebMethod(Description = "app登录方法")]
        public void AppLoginV2(string loginName, string loginPass)
        {
            HttpContext.Current.Response.Write(new ApiServiceApp().AppLogin(loginName, loginPass));
        }


        [WebMethod(Description = "app注册门店")]
        public void AppRegV2(string orgFullName,
           string F_Description, string MemberNum, int SeatNum,
            string loginName, string loginPass, string F_RealName)
        {
            HttpContext.Current.Response.Write(new ApiServiceApp().AppRegsiter( orgFullName, F_Description,  MemberNum,  SeatNum,loginName,  loginPass,  F_RealName));
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}

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

        /// <summary>
        /// 处理订单
        /// </summary>
        /// <param name="_json"></param>
        [WebMethod(Description = "订单提交处理")]
        public void ProcessingOrders(string _json, string op)
        {
            HttpContext.Current.Response.Write(new ApiServiceApp().ProcessingOrders(_json,op));
        }

        /// <summary>
        /// 获取当天的所有订单号
        /// </summary>
        /// <param name="_pageIndex"></param>
        /// <param name="_pageSize"></param>
        /// <param name="_orgid"></param>
        [WebMethod(Description = "获取当天该组织的所有订单")]
        public void GetSimpleOrderList(string _pageIndex, string _pageSize, string _orgid)
        {
            HttpContext.Current.Response.Write(new ApiServiceApp().GetSimpleOrderList(_orgid));
        }

        /// <summary>
        /// 获取台位及员工
        /// </summary>
        /// <param name="_org">所属组织架构</param>
        [WebMethod(Description = "台位名和员工编号")]
        public void GetSeatANDMember(string _orgid)
        {
            HttpContext.Current.Response.Write(new ApiServiceApp().GetSeatsAndMembers(_orgid));
        }

        /// <summary>
        /// 获取商品分类以下的所有商品信息
        /// </summary>
        /// <param name="_pageIndex">页码，从1开始</param>
        /// <param name="_pageSize">页大小</param>
        /// <param name="_oid">商品分类主键标识</param>
        /// <param name="_orgid">所属组织架构</param>
        [WebMethod(Description = "获取商品分类以下的所有商品信息")]
        public void GetProductListByPCategory(string _pageIndex, string _pageSize, string _oid, string _orgid)
        {
            HttpContext.Current.Response.Write(new ApiServiceApp().GetProductListByPCategory(_pageIndex,_pageSize,_oid,_orgid));
        }

        /// <summary>
        /// 获取商品类别
        /// </summary>
        /// <param name="_org">所属组织架构</param>
        [WebMethod(Description = "获取商品类别")]
        public void GetProductCateoryList(string _orgid)
        {
            HttpContext.Current.Response.Write(new ApiServiceApp().GetProductCategoryList(_orgid));
        }

        /// <summary>
        /// 分页获取组织架构简单列表
        /// </summary>
        /// <param name="_pageIndex">页码，从1开始</param>
        /// <param name="_pageSize">页大小</param>
        /// <param name="_cname">名称，模糊查询</param>
        [WebMethod(Description = "分页获取组织架构简单列表")]
        public void GetOrgSimpleList(string _pageIndex, string _pageSize, string _cname)
        {
            HttpContext.Current.Response.Write(new ApiServiceApp().GetOrgSimpleList(_pageIndex,_pageSize,_cname));
        }

        [WebMethod(Description = "单个订单精确查询")]
        public void GetOrderInfo(string _OrderNo)
        {
            HttpContext.Current.Response.Write(new ApiServiceApp().GetOrderInfo(_OrderNo));
        }

        /// <summary>
        /// 获取订单号
        /// </summary>
        /// <param name="_orgid"></param>
        [WebMethod(Description = "获取订单号")]
        public void GetOrderNo(string _orgid)
        {
            HttpContext.Current.Response.Write(new ApiServiceApp().GetOrderNo(_orgid));
        }

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

        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}
    }
}

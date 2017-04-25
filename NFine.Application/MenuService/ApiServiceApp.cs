using Newtonsoft.Json;
using NFine.Application;
using NFine.Application.SystemManage;
using NFine.Application.SystemSecurity;
using NFine.Code;
using NFine.Data.Extensions;
using NFine.Domain._02_ViewModel;
using NFine.Domain._03_Entity.MenuBiz;
using NFine.Domain._04_IRepository.MenuBiz;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.Entity.SystemSecurity;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.MenuBiz;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.MenuService
{
    /// <summary>
    /// app终端接口服务类
    /// </summary>
    public class ApiServiceApp
    {
        #region 组织架构底层服务类
        private IOrganizeRepository orgDllService = new OrganizeRepository();
        private IUserRepository userDllService = new UserRepository();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();

        #endregion

        #region 订单处理底层服务类
        private IT_ORDERRepository orderservice = new T_ORDERRepository();
        private IT_ORDER_INFORepository orderInfoService = new T_ORDER_INFORepository();
        private IT_ORDER_CHECKOUTRepository checkOutInfoService = new T_ORDER_CHECKOUTRepository();
        #endregion

        #region 商品相关底层服务类
        private IT_PRODUCTRepository productService = new T_PRODUCTRepository();
        private IT_PRODUCT_CATEORYRepository objCategoryservice = new T_PRODUCT_CATEORYRepository();
        private ISYS_FILESRepository fileService = new SYS_FILESRepository();
        private IRS_PRODUCT_FILERepository fileRsProductService = new RS_PRODUCT_FILERepository();
        #endregion

        #region 台位和员工底层服务类
        private IT_SEATRepository seatService = new T_SEATRepository();
        private IT_MEMBERSRepository memberService = new T_MEMBERSRepository();
        #endregion


        #region 私有工具方法

        public List<SimpleOrder> OrderFullTableToList2(DataTable dt)
        {
            List<SimpleOrder> modelList = new List<SimpleOrder>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SimpleOrder model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SimpleOrder();
                    if (dt.Rows[n]["OrderNo"].ToString() != "")
                    {
                        model.orderNo = dt.Rows[n]["OrderNo"].ToString();
                    }
                    if (dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
                    }
                    model.Seat = dt.Rows[n]["Seat"].ToString();
                    if (dt.Rows[n]["PeopleNum"].ToString() != "")
                    {
                        model.PeopleNum = int.Parse(dt.Rows[n]["PeopleNum"].ToString());
                    }
                    model.MemberName = dt.Rows[n]["MemberName"].ToString();
                    model.Dec = dt.Rows[n]["Dec"].ToString();
                    //string sql = "select * from T_ORDER_INFO where OrderNo='" + model.orderNo + "'";
                    //DataTable df = (DBUtility.DbHelperSQL.Query(sql)).Tables[0];
                    //model.orderInfo = TableToList3(df);
                    //modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 员工数据表转对象列表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<T_MEMBERSEntity> MembersTableToList(DataTable dt)
        {
            List<T_MEMBERSEntity> modelList = new List<T_MEMBERSEntity>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                T_MEMBERSEntity model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new T_MEMBERSEntity();
                    if (dt.Rows[n]["OID"].ToString() != "")
                    {
                        model.OID = int.Parse(dt.Rows[n]["OID"].ToString());
                    }
                    if (dt.Rows[n]["CreateOn"].ToString() != "")
                    {
                        model.CreateOn = DateTime.Parse(dt.Rows[n]["CreateOn"].ToString());
                    }
                    if (dt.Rows[n]["CreateUserId"].ToString() != "")
                    {
                        model.CreateUserId = int.Parse(dt.Rows[n]["CreateUserId"].ToString());
                    }
                    model.CreateBy = dt.Rows[n]["CreateBy"].ToString();
                    if (dt.Rows[n]["ModifiedOn"].ToString() != "")
                    {
                        model.ModifiedOn = DateTime.Parse(dt.Rows[n]["ModifiedOn"].ToString());
                    }
                    if (dt.Rows[n]["ModifiedUserId"].ToString() != "")
                    {
                        model.ModifiedUserId = int.Parse(dt.Rows[n]["ModifiedUserId"].ToString());
                    }
                    model.ModifiedBy = dt.Rows[n]["ModifiedBy"].ToString();
                    model.Cname = dt.Rows[n]["Cname"].ToString();
                    model.Gender = dt.Rows[n]["Gender"].ToString();
                    model.Introduction = dt.Rows[n]["Introduction"].ToString();
                    if (dt.Rows[n]["OrgID"].ToString() != "")
                    {
                        model.OrgID = int.Parse(dt.Rows[n]["OrgID"].ToString());
                    }
                    if (dt.Rows[n]["DeletionStateCode"].ToString() != "")
                    {
                        model.DeletionStateCode = int.Parse(dt.Rows[n]["DeletionStateCode"].ToString());
                    }
                    if (dt.Rows[n]["Enabled"].ToString() != "")
                    {
                        model.Enabled = int.Parse(dt.Rows[n]["Enabled"].ToString());
                    }
                    if (dt.Rows[n]["SortCode"].ToString() != "")
                    {
                        model.SortCode = int.Parse(dt.Rows[n]["SortCode"].ToString());
                    }
                    model.Description = dt.Rows[n]["Description"].ToString();


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 台位数据列表转为对象列表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<T_SEATEntity> SeatTableToList(DataTable dt)
        {
            List<T_SEATEntity> modelList = new List<T_SEATEntity>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                T_SEATEntity model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new T_SEATEntity();
                    if (dt.Rows[n]["OID"].ToString() != "")
                    {
                        model.OID = int.Parse(dt.Rows[n]["OID"].ToString());
                    }
                    if (dt.Rows[n]["ParentID"].ToString() != "")
                    {
                        model.ParentID = int.Parse(dt.Rows[n]["ParentID"].ToString());
                    }
                    model.SeatNo = dt.Rows[n]["SeatNo"].ToString();
                    model.SaatCategory = dt.Rows[n]["SaatCategory"].ToString();
                    if (dt.Rows[n]["PersonNum"].ToString() != "")
                    {
                        model.PersonNum = int.Parse(dt.Rows[n]["PersonNum"].ToString());
                    }
                    if (dt.Rows[n]["OrgID"].ToString() != "")
                    {
                        model.OrgID = int.Parse(dt.Rows[n]["OrgID"].ToString());
                    }
                    model.Desc = dt.Rows[n]["Desc"].ToString();
                    model.Status = dt.Rows[n]["Status"].ToString();


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        private List<SimpleProductCateory> CategoryTableToList(DataTable dt)
        {
            List<SimpleProductCateory> modelList = new List<SimpleProductCateory>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SimpleProductCateory model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SimpleProductCateory();
                    if (dt.Rows[n]["OID"].ToString() != "")
                    {
                        model.OID = dt.Rows[n]["OID"].ToString();
                    }
                    model.CName = dt.Rows[n]["CName"].ToString();


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 订单对象，数据表转对象
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<T_ORDEREntity> TableToList(DataTable dt)
        {
            List<T_ORDEREntity> modelList = new List<T_ORDEREntity>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                T_ORDEREntity model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new T_ORDEREntity();
                    if (dt.Rows[n]["OID"].ToString() != "")
                    {
                        model.OID = int.Parse(dt.Rows[n]["OID"].ToString());
                    }
                    model.OrderNo = dt.Rows[n]["OrderNo"].ToString();
                    if (dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
                    }
                    model.Seat = dt.Rows[n]["Seat"].ToString();
                    if (dt.Rows[n]["PeopleNum"].ToString() != "")
                    {
                        model.PeopleNum = int.Parse(dt.Rows[n]["PeopleNum"].ToString());
                    }
                    model.MemberName = dt.Rows[n]["MemberName"].ToString();
                    model.Dec = dt.Rows[n]["Dec"].ToString();
                    if (dt.Rows[n]["OrderState"].ToString() != "")
                    {
                        model.OrderState = int.Parse(dt.Rows[n]["OrderState"].ToString());
                    }
                    if (dt.Rows[n]["ModifiedOn"].ToString() != "")
                    {
                        model.ModifiedOn = DateTime.Parse(dt.Rows[n]["ModifiedOn"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获取后几位数
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="num">返回的具体位数</param>
        /// <returns>返回结果的字符串</returns>
        private string GetLastStr(string str, int num)
        {
            int count = 0;
            if (str.Length > num)
            {
                count = str.Length - num;
                str = str.Substring(count, num);
            }
            return str;
        }
        #endregion

        public string ProcessingOrders(string _json, string op)
        {
            string resultString = "";
            ReturnPageResult<SimpleOrder> result = new ReturnPageResult<SimpleOrder>();
            result.ResultCode = "-1";
            result.Msg = "查询失败";
            try
            {
                readJSON rj = new readJSON();
                result.Msg = rj.readJ3("[" + _json + "]", op);
                if (result.Msg == "提交成功！")
                {
                    result.ResultCode = "200";
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = "-1";
                result.Msg = "查询失败:" + ex.ToString();
            }
            resultString = JsonConvert.SerializeObject(result);
            return resultString;
        }

        /// <summary>
        /// 获取指定门店当天的订单列表
        /// </summary>
        /// <param name="_orgid"></param>
        /// <returns></returns>
        public string GetSimpleOrderList(string _orgid)
        {
            string resultString = "";
            ReturnPageResult<SimpleOrder> result = new ReturnPageResult<SimpleOrder>();
            result.ResultCode = "-1";
            result.Msg = "查询失败";
            try
            {
                T_ORDEREntity to = new T_ORDEREntity();
                PageObject<SimpleOrder> page = new PageObject<SimpleOrder>();
                string time1 = DateTime.Now.ToString("yyyy - MM - dd");//获取时间日期
                string sql = "select T_ORDER.*,T_ORDER_INFO.* "+
                        " from T_ORDER, T_ORDER_INFO  where T_ORDER.OrgID = "+ _orgid + " and " +
                        " T_ORDER.OrderState = 1 and T_ORDER.CreateTime " +
                        "   Between '"+ time1 + " 0:00:00' " +
                        " and '"+ time1 + " 23:59:59' " +
                        " and T_ORDER_INFO.OrderNo = T_ORDER.OrderNo " +
                        " order by T_ORDER.CreateTime desc";
                DataTable dt = DbHelper.QueryDataTable(sql);

                List<SimpleOrder> orderList = new List<SimpleOrder>();
                foreach (DataRow item in dt.Rows)
                {
                    SimpleOrder odr = new SimpleOrder();
                    if (item["OrderNo"].ToString() != "")
                    {
                        odr.orderNo = item["OrderNo"].ToString();
                    }

                    int count =  orderList.Count(t => t.orderNo == odr.orderNo);
                    if (count > 0)
                    {
                        continue;
                    }

                    if (item["CreateTime"].ToString() != "")
                    {
                        odr.CreateTime = DateTime.Parse(item["CreateTime"].ToString());
                    }
                    odr.Seat = item["Seat"].ToString();
                    if (item["PeopleNum"].ToString() != "")
                    {
                        odr.PeopleNum = int.Parse(item["PeopleNum"].ToString());
                    }
                    odr.MemberName = item["MemberName"].ToString();
                    odr.Dec = item["Dec"].ToString();
                    orderList.Add(odr);
                }
                List<T_ORDER_INFOEntity> infoList = new List<T_ORDER_INFOEntity>();
                foreach (DataRow infoItem in dt.Rows)
                {
                    T_ORDER_INFOEntity info = new T_ORDER_INFOEntity();
                    if (infoItem["OID"].ToString() != "")
                    {
                        info.OID = int.Parse(infoItem["OID"].ToString());
                    }
                    info.OrderNo = infoItem["OrderNo"].ToString();
                    info.ProductId = infoItem["ProductId"].ToString();
                    info.ProductCname = infoItem["ProductCname"].ToString();
                    if (infoItem["PNum"].ToString() != "")
                    {
                        info.PNum = float.Parse(infoItem["PNum"].ToString());
                    }
                    if (infoItem["Price1"].ToString() != "")
                    {
                        info.Price1 = decimal.Parse(infoItem["Price1"].ToString());
                    }
                    if (infoItem["Price2"].ToString() != "")
                    {
                        info.Price2 = decimal.Parse(infoItem["Price2"].ToString());
                    }
                    info.MemberName = infoItem["MemberName"].ToString();
                    if (infoItem["CreateOn"].ToString() != "")
                    {
                        info.CreateOn = DateTime.Parse(infoItem["CreateOn"].ToString());
                    }
                    if (infoItem["ModifiedOn"].ToString() != "")
                    {
                        info.ModifiedOn = DateTime.Parse(infoItem["ModifiedOn"].ToString());
                    }
                    infoList.Add(info);
                }

                foreach (SimpleOrder item in orderList)
                {
                    item.orderInfo = new List<T_ORDER_INFOEntity>();
                    foreach (T_ORDER_INFOEntity infoItem in infoList)
                    {
                        if (infoItem.OrderNo == item.orderNo)
                        {
                            item.orderInfo.Add(infoItem);
                        }
                    }
                }
                
                page.Data = orderList;
                result.Page = page;
                result.ResultCode = "200";
                result.Msg = "查询成功";
            }
            catch (Exception ex)
            {
                result.ResultCode = "-1";
                result.Msg = "查询失败:" + ex.ToString();
            }
            resultString = JsonConvert.SerializeObject(result);
            return resultString;
        }

        /// <summary>
        /// 获取台位和员工信息
        /// </summary>
        /// <param name="_orgid"></param>
        /// <returns></returns>
        public string GetSeatsAndMembers(string _orgid)
        {
            string resultString = "";

            ReturnPageResult<ApiSeatsAndEmpsModel> result = new ReturnPageResult<ApiSeatsAndEmpsModel>();

            result.ResultCode = "-1";
            result.Msg = "查询失败";
            try
            {
               ApiSeatsAndEmpsModel page = new ApiSeatsAndEmpsModel();
                string sql = "SELECT * FROM      T_MEMBERS WHERE   OrgID ='" + _orgid + "'";
                List<T_MEMBERSEntity> Members = memberService.FindList(sql);
                string sql2 = " select * from T_SEAT WHERE   OrgID ='" + _orgid + "'";
                List<T_SEATEntity> Seats = seatService.FindList(sql2);

                page.Seats = Seats;
                page.Members = Members;

                result.Data1 = page;
                result.ResultCode = "200";
                result.Msg = "查询成功";
            }
            catch (Exception ex)
            {

                result.ResultCode = "-1";
                result.Msg = "查询失败:" + ex.ToString();
            }
            resultString = JsonConvert.SerializeObject(result);
            return resultString;
        }

        /// <summary>
        /// 根据类别查询商品
        /// </summary>
        /// <param name="_pageIndex"></param>
        /// <param name="_pageSize"></param>
        /// <param name="_oid"></param>
        /// <param name="_orgid"></param>
        /// <returns></returns>
        public string GetProductListByPCategory(string _pageIndex,string _pageSize,string _oid,string _orgid)
        {
            string resultString = "";

            ReturnPageResult<SimpleProduct> result = new ReturnPageResult<SimpleProduct>();
            result.ResultCode = "-1";
            result.Msg = "查询失败";
            try
            {
                T_PRODUCTEntity tpc = new T_PRODUCTEntity();
                PageObject<SimpleProduct> page = new PageObject<SimpleProduct>();//tpc.GetPage(_pageIndex, _pageSize, _oid, _orgid);

                string sql = "select * from T_PRODUCT where PCategory = "+ _oid + " and OrgID = "+ _orgid + "";
                List<T_PRODUCTEntity> list =   productService.FindList(sql);
                List<SimpleProduct> listSimpleProduct = new List<SimpleProduct>();
                foreach (var item in list)
                {
                    SimpleProduct sp = new SimpleProduct();
                    sp.OID = item.OID.ToString();
                    sp.CName = item.CName;
                    sp.Price1 = item.Price1.ToString() ;
                    sp.Price2 = item.Price2.ToString();
                    sp.PIngredients = item.PContent;
                    sp.PContent = item.PContent;
                    sp.PPractice = item.PContent;
                    sp.ImgName = item.PriceString;
                    listSimpleProduct.Add(sp);
                }
                page.Data = listSimpleProduct;
                result.Page = page;
                result.ResultCode = "200";
                result.Msg = "查询成功";
            }
            catch (Exception ex)
            {

                result.ResultCode = "-1";
                result.Msg = "查询失败:" + ex.ToString();
            }
            resultString = JsonConvert.SerializeObject(result);
            return resultString;
        }

        /// <summary>
        /// 获取商品类别
        /// </summary>
        /// <param name="_orgid"></param>
        /// <returns></returns>
        public string GetProductCategoryList(string _orgid)
        {
            string resultString = "";

            ReturnPageResult<SimpleProductCateory> result = new ReturnPageResult<SimpleProductCateory>();
            result.ResultCode = "-1";
            result.Msg = "查询失败";
            try
            {
                T_PRODUCT_CATEORYEntity tpc = new T_PRODUCT_CATEORYEntity();
                PageObject<SimpleProductCateory> page = new PageObject<SimpleProductCateory>(); //tpc.GetPage(_orgid);
                string sql = "select OID,CName from T_PRODUCT_CATEORY where OrgID =" + _orgid;
                DataTable dt = DbHelper.QueryDataTable(sql);
                List<SimpleProductCateory> list = CategoryTableToList(dt);
                page.Data = list;
                result.Page = page;
                result.ResultCode = "200";
                result.Msg = "查询成功";
            }
            catch (Exception ex)
            {
                result.ResultCode = "-1";
                result.Msg = "查询失败:" + ex.ToString();
            }
            resultString = JsonConvert.SerializeObject(result);
            return resultString;
        }

        /// <summary>
        /// 获取简单的组织架构信息
        /// </summary>
        /// <param name="_pageIndex"></param>
        /// <param name="_pageSize"></param>
        /// <param name="_cname"></param>
        /// <returns></returns>
        public string GetOrgSimpleList(string _pageIndex,string _pageSize,string _cname)
        {
            ReturnPageResult<SimpleOrgEntity> result = new ReturnPageResult<SimpleOrgEntity>();
            try
            {
                string sql = "select * from Sys_Organize where F_EnCode = 'Store' ";
                if (!string.IsNullOrEmpty(_cname))
                {
                    sql += " and F_FullName like '%"+_cname+"%'";
                }

                List<OrganizeEntity> list = orgDllService.FindList(sql);
                PageObject<SimpleOrgEntity> page = new PageObject<SimpleOrgEntity>();
                List <SimpleOrgEntity> simpleList   = new List<SimpleOrgEntity>();
                foreach (var item in list)
                {
                    SimpleOrgEntity se = new SimpleOrgEntity();
                    se.OID = item.OrgNo.ToString();
                    se.LongName = item.F_FullName;
                    simpleList.Add(se);
                }
                page.Data = simpleList;
                result.Page = page;
                result.ResultCode = "200";
                result.Msg = "查询成功";
            }
            catch (Exception ex)
            {
                result.ResultCode = "-1";
                result.Msg = "异常:"+ex;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 自动生成订单号返回
        /// </summary>
        /// <param name="_orgid"></param>
        /// <returns></returns>
        public string GetOrderNo(string _orgid)
        {
            ReturnPageResult<T_ORDEREntity> result = new ReturnPageResult<T_ORDEREntity>();
            result.ResultCode = "-1";
            result.Msg = "查询失败";
            try
            {
                T_ORDEREntity mto = new T_ORDEREntity();
               // better.BLL.T_ORDER bto = new T_ORDER();
                string orderNo = string.Empty;//返回的字符串
                string adoString = string.Empty;//临时字符串
                string time1 = DateTime.Now.ToLongDateString();
                string sql = "select top 1 * FROM      T_ORDER where OrderNo like '" + _orgid + "%' and CreateTime > '" + Convert.ToDateTime(time1) + "' ORDER BY CreateTime DESC";

                DataTable dt = DbHelper.QueryDataTable(sql);//(DBUtility.DbHelperSQL.Query(sql)).Tables[0];
                List<T_ORDEREntity> lmto = TableToList(dt);
                if (lmto.Count == 0)
                {
                    mto.OrderNo = orderNo = _orgid + DateTime.Now.ToString("yyyyMMdd") + "10001";
                    mto.CreateTime = DateTime.Now;
                    mto.OrgID = int.Parse(_orgid);
                    mto.Seat = "--";
                    mto.PeopleNum = 0;
                    mto.MemberName = "--";
                    mto.Dec = "--";
                    mto.OrderState = 0;
                    mto.ModifiedOn = DateTime.Now;
                    orderservice.Insert(mto);//bto.Add(mto);//将新生成的订单号存进数据库
                }
                else
                {
                    foreach (var item in lmto)
                    {
                        adoString = GetLastStr(item.OrderNo, 5);
                    }
                    int num = Convert.ToInt32(adoString) + 1;
                    mto.OrderNo = orderNo = _orgid + DateTime.Now.ToString("yyyyMMdd") + num.ToString();
                    mto.CreateTime = DateTime.Now;
                    mto.OrgID = int.Parse(_orgid);
                    mto.Seat = "--";
                    mto.PeopleNum = 0;
                    mto.MemberName = "--";
                    mto.Dec = "--";
                    mto.OrderState = 0;
                    mto.ModifiedOn = DateTime.Now;
                    orderservice.Insert(mto);//将新生成的订单号存进数据库
                }
                result.strNo = mto.OrderNo;
                result.Msg = "生成订单号成功";
                result.ResultCode = "200";
            }
            catch (Exception ex)
            {
                result.ResultCode = "-1";
                result.Msg = "异常:"+ex;
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 查询指定单号订单明细信息
        /// </summary>
        /// <param name="_OrderNo"></param>
        /// <returns></returns>
        public string GetOrderInfo(string _OrderNo)
        {
            ReturnPageResult<ApiOrderAndInfosModel> result = new ReturnPageResult<ApiOrderAndInfosModel>();
            try
            {
                ApiOrderAndInfosModel objApiOrderAndInfosModel = new ApiOrderAndInfosModel();
                objApiOrderAndInfosModel.order = orderservice.FindList(" select * from T_ORDER WHERE   OrderNo ='" + _OrderNo + "'");
                objApiOrderAndInfosModel.order_info = orderInfoService.FindList("SELECT * FROM T_ORDER_INFO WHERE   OrderNo ='" + _OrderNo + "'");
                result.Data1 = objApiOrderAndInfosModel;
                result.Msg = "查询成功";
                result.ResultCode = "200";
            }
            catch (Exception ex)
            {
                result.Msg = "异常:"+ex;
                result.ResultCode = "-1";
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// App登录
        /// </summary>
        /// <returns></returns>
        public string AppLogin(string username, string password)
        {
            ReturnSimpleResult2<AppRegsiterResult> ret = new ReturnSimpleResult2<AppRegsiterResult>();
            try
            {
                LogEntity logEntity = new LogEntity();
                logEntity.F_ModuleName = "App登录";
                logEntity.F_Type = DbLogType.Login.ToString();
                UserEntity userEntity = new UserApp().CheckLogin(username, password);
                if (userEntity != null)
                {
                    AppRegsiterResult vm = new AppRegsiterResult();
                    vm.CurrentUser = userEntity;
                    OrganizeEntity orgEntity = orgDllService.FindEntity(t => t.F_Id == userEntity.F_OrganizeId);
                    vm.CurrentOrganizeEntity = orgEntity;
                    ret.Msg = "登录成功";
                    ret.ResultCode = "200";
                    ret.t = vm;

                    #region 写登录日志
                    logEntity.F_Account = userEntity.F_Account;
                    logEntity.F_NickName = userEntity.F_RealName;
                    logEntity.F_Result = true;
                    logEntity.F_Description = "登录成功";
                    new LogApp().WriteDbLog(logEntity);
                    #endregion
                }
                else
                {
                    ret.Msg = "登录失败";
                    ret.ResultCode = "-1";
                    ret.t = null;

                    #region 写登录日志
                    logEntity.F_Account = userEntity.F_Account;
                    logEntity.F_Result = false;
                    logEntity.F_Description = "登录失败";
                    new LogApp().WriteDbLog(logEntity);
                    #endregion  
                }
            }
            catch (Exception ex)
            {
                ret.Msg = ex.ToString();
                ret.ResultCode = "-1";
                ret.t = null;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(ret);
        }


        /// <summary>
        /// app终端门店注册方法
        /// </summary>
        /// <param name="orgFullName"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public string AppRegsiter(string orgFullName, string F_Description,string MemberNum,int SeatNum,
            string loginName,string loginPass,string F_RealName)
        {
            ReturnSimpleResult2<AppRegsiterResult> ret = new ReturnSimpleResult2<AppRegsiterResult>();
            try
            {
                OrganizeEntity findOrg = orgDllService.FindEntity(t => t.F_FullName == orgFullName);
                UserEntity findUser = userDllService.FindEntity(t => t.F_Account == loginName);
                if (findOrg!=null) //判断店名，同名的不能注册
                {
                    ret.Msg = "已经存在同名的简称或者全称，注册失败!";
                    ret.ResultCode = "-1";
                    ret.t = null;
                }
                else if (findUser!=null) //判断登录名，同名的不能注册
                {
                    ret.Msg = "已经存在同名的登录账户，请更换一个";
                    ret.ResultCode = "-1";
                    ret.t = null;
                }
                else
                {
                    //添加门店
                    OrganizeEntity addOrg = new OrganizeEntity();
                    int OrgNo = orgDllService.IQueryable().Max(x => x.OrgNo);
                    addOrg.OrgNo = OrgNo+1;
                    addOrg.F_ParentId = "80e0601b-92f1-4399-99af-f2164a852a33";
                    addOrg.F_Layers = 2;
                    addOrg.F_EnCode = "Store";
                    addOrg.F_FullName = orgFullName;
                    addOrg.F_ShortName = orgFullName;
                    addOrg.F_CategoryId = "qita";
                    addOrg.F_ManagerId = F_RealName;
                    addOrg.F_TelePhone = loginName;
                    addOrg.F_MobilePhone = loginName;
                    addOrg.F_WeChat = "@";
                    addOrg.F_Fax = "0";
                    addOrg.F_Email = "@";
                    addOrg.F_AreaId = "450000";
                    addOrg.F_Address = "China";
                    addOrg.F_AllowEdit = true;
                    addOrg.F_AllowDelete = true;
                    addOrg.F_SortCode += orgDllService.IQueryable().Max(x => x.F_SortCode);
                    addOrg.F_DeleteMark = false;
                    addOrg.F_EnabledMark = true;
                    addOrg.F_Description = F_Description;
                    addOrg.MemberNum = MemberNum;
                    addOrg.SeatNum = SeatNum;
                    addOrg.Create();
                    int excCount = orgDllService.Insert(addOrg);
                    if (excCount > 0) //门店添加成功
                    {
                        UserEntity addUser = new UserEntity();
                        addUser.F_Account = loginName;
                        addUser.F_RealName = F_RealName;
                        addUser.F_Gender = true;
                        addUser.F_Birthday = DateTime.Parse( "2000-01-01");
                        addUser.F_MobilePhone = loginName;
                        addUser.F_Email = "@";
                        addUser.F_WeChat = "0";
                        addUser.F_ManagerId = "0";
                        addUser.F_SecurityLevel = 0;
                        addUser.F_Signature = "0";
                        addUser.F_OrganizeId = addOrg.F_Id;
                        //所述机构业务代码
                        addUser.OrgNo = addOrg.OrgNo;
                        addUser.F_DepartmentId = "1";
                        addUser.F_RoleId = "8f4e2e54-2d55-4cb0-85f0-fdbde06c0d60";//门店经理角色
                        addUser.F_DutyId = "1";
                        addUser.F_IsAdministrator = false;
                        addUser.F_SortCode = 1;
                        addUser.F_DeleteMark = false;
                        addUser.F_EnabledMark = true;
                        addUser.F_Description = "终端注册用户";
                        addUser.Create();

                        UserLogOnEntity addUserLog = new UserLogOnEntity();
                        addUserLog.F_UserPassword = loginPass;
                        userDllService.SubmitForm(addUser, addUserLog, ""); 

                        AppRegsiterResult vm = new AppRegsiterResult();
                        vm.CurrentOrganizeEntity = addOrg;
                        vm.CurrentUser = addUser;
                        ret.Msg = "操作成功";
                        ret.ResultCode = "200";
                        ret.t = vm;

                        //发送短信通知
                        SMSTools.Send(System.Configuration.ConfigurationManager.AppSettings["adminMobile"].ToString(), "有新用户注册，账户名为"+ loginName + "，请关注并指导如何使用我们产品！【点菜了】");
                        SMSTools.Send(loginName, "欢迎您注册使用点菜了智能点菜app，登录账户为"+ loginName + "，初始密码123456，管理后台地址：http://nnbetter.com:8029，请详细查看app首页的使用说明，可以通过添加微信duncansailing获得免费技术指导。【点菜了】");
                    }
                }
            }
            catch (Exception ex)
            {
                ret.Msg = ex.ToString();
                ret.ResultCode = "-1";
                ret.t = null;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(ret);
        }
    }
}

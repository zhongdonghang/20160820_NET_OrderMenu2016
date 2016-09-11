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
        private IOrganizeRepository orgDllService = new OrganizeRepository();
        private IUserRepository userDllService = new UserRepository();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();


        #region 订单处理底层服务类
        private IT_ORDERRepository orderservice = new T_ORDERRepository();
        private IT_ORDER_INFORepository orderInfoService = new T_ORDER_INFORepository();
        private IT_ORDER_CHECKOUTRepository checkOutInfoService = new T_ORDER_CHECKOUTRepository();
        #endregion




        /// <summary>
        /// 订单对象，数据表转对象
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<T_ORDEREntity> TableToList(DataTable dt)
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
        public string GetLastStr(string str, int num)
        {
            int count = 0;
            if (str.Length > num)
            {
                count = str.Length - num;
                str = str.Substring(count, num);
            }
            return str;
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
                    addOrg.F_Address = "广西";
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

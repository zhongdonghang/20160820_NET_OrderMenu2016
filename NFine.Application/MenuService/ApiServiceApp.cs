using NFine.Application;
using NFine.Application.SystemManage;
using NFine.Application.SystemSecurity;
using NFine.Code;
using NFine.Domain._02_ViewModel;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.Entity.SystemSecurity;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
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

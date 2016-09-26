using NFine.Code;
using NFine.Domain._02_ViewModel;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.MenuService
{
    public class UserInfoApp
    {
        private IUserRepository userService = new UserRepository();
        private IUserLogOnRepository userLogOnService = new UserLogOnRepository();

        public ModifyPasswordViewModel GetByUserID(string userID)
        {
            ModifyPasswordViewModel vm = new ModifyPasswordViewModel();
            vm.UserAccount = userService.FindEntity(userID).F_Account;
            return vm;
        }

        public bool ModifyPassword(string oldPass,string newPass)
        {
            bool isTrue = false;
          string UserId =  OperatorProvider.Provider.GetCurrent().UserId;
            UserLogOnEntity objUserLogOnEntity  =  userLogOnService.IQueryable(t => t.F_UserId == UserId).First();
           // UserLogOnEntity objUserLogOnEntity = userLogOnService.FindEntity(t => t.F_UserId == OperatorProvider.Provider.GetCurrent().UserId);
            if (objUserLogOnEntity.F_UserPassword == oldPass)
            {
                objUserLogOnEntity.F_UserPassword = newPass;
                userLogOnService.Update(objUserLogOnEntity);
                isTrue = true;
            }
            return isTrue;
        }

    }
}

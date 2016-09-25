/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFine.Application.SystemManage
{
    public class OrganizeApp
    {
        private IOrganizeRepository service = new OrganizeRepository();

        public void Modify(OrganizeEntity newOrganizeEntity)
        {
            OrganizeEntity oldObj = service.FindEntity(newOrganizeEntity.F_Id);
            oldObj.F_FullName = newOrganizeEntity.F_FullName;
            oldObj.F_ShortName = newOrganizeEntity.F_FullName;
            oldObj.F_TelePhone = newOrganizeEntity.F_TelePhone;
            oldObj.F_MobilePhone = newOrganizeEntity.F_MobilePhone;
            oldObj.F_WeChat = newOrganizeEntity.F_WeChat;
            oldObj.F_Fax = newOrganizeEntity.F_Fax;
            oldObj.F_Email = newOrganizeEntity.F_Email;
            oldObj.F_Address = newOrganizeEntity.F_Address;
            oldObj.MemberNum = newOrganizeEntity.MemberNum;
            oldObj.SeatNum = newOrganizeEntity.SeatNum;
            oldObj.F_Description = newOrganizeEntity.F_Description;
            service.Update(oldObj);
        }

        public OrganizeEntity GetByOrgNo(int OrgNo)
        {
            return service.IQueryable().Where<OrganizeEntity>(t => t.OrgNo == OrgNo).First();
        }

        public List<OrganizeEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.F_CreatorTime).ToList();
        }
        public OrganizeEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.F_Id == keyValue);
            }
        }
        public void SubmitForm(OrganizeEntity organizeEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                organizeEntity.Modify(keyValue);
                service.Update(organizeEntity);
            }
            else
            {
                ///钟东航修改，增加门店编号，自己写程序获取自增，不用数据库自增
                int OrgNo =  service.IQueryable().Max(x => x.OrgNo);
                organizeEntity.OrgNo = OrgNo;
                organizeEntity.Create();
                service.Insert(organizeEntity);
            }
        }
    }
}

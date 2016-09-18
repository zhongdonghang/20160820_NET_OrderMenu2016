using NFine.Code;
using NFine.Domain._03_Entity.MenuBiz;
using NFine.Domain._04_IRepository.MenuBiz;
using NFine.Repository.MenuBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.MenuService
{
    public class MembersApp
    {
        private IT_MEMBERSRepository service = new T_MEMBERSRepository();

        /// <summary>
        /// 分页按查询出服务员列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public List<T_MEMBERSEntity> GetList(Pagination pagination, string keyword, int OrgId)
        {
            var expression = ExtLinq.True<T_MEMBERSEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Cname.Contains(keyword));
            }
            expression = expression.And(t => t.OrgID == OrgId);
            return service.FindList(expression, pagination);
        }

        /// <summary>
        /// 按组织机构查询出所有服务员列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public List<T_MEMBERSEntity> GetList(string keyword, int OrgId)
        {
            var expression = ExtLinq.True<T_MEMBERSEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Cname.Contains(keyword));
            }
            expression = expression.And(t => t.OrgID == OrgId);
            Pagination pagination = new Pagination();
            pagination.sidx = "SortCode desc";
            pagination.sord = "asc";
            pagination.rows = 100;
            pagination.page = 1;

            return service.FindList(expression, pagination);
        }

        /// <summary>
        /// 查询单个服务员信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public T_MEMBERSEntity GetForm(string keyValue)
        {
            return service.FindEntity(int.Parse(keyValue));
        }

        public void DeleteForm(string keyValue)
        {
            int deleteOID = int.Parse(keyValue);
            service.Delete(t => t.OID == deleteOID);
        }

        public void SubmitForm(T_MEMBERSEntity objT_MEMBERSEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))//编辑
            {
                T_MEMBERSEntity oblT_MEMBERSEntity = service.FindEntity(int.Parse(keyValue));
                oblT_MEMBERSEntity.Cname = objT_MEMBERSEntity.Cname;
                oblT_MEMBERSEntity.SortCode = objT_MEMBERSEntity.SortCode;
                oblT_MEMBERSEntity.Description = objT_MEMBERSEntity.Description == null ? "暂无" : objT_MEMBERSEntity.Description;
                oblT_MEMBERSEntity.Introduction = objT_MEMBERSEntity.Description;
                oblT_MEMBERSEntity.Gender = objT_MEMBERSEntity.Gender;
                service.Update(oblT_MEMBERSEntity);
            }
            else
            {
                objT_MEMBERSEntity.DeletionStateCode = 0;
                objT_MEMBERSEntity.Enabled = 0;
                objT_MEMBERSEntity.Description = objT_MEMBERSEntity.Description==null?"暂无": objT_MEMBERSEntity.Description;
                objT_MEMBERSEntity.Introduction = objT_MEMBERSEntity.Description;
                objT_MEMBERSEntity.CreateUserId = 0;
                objT_MEMBERSEntity.CreateBy = OperatorProvider.Provider.GetCurrent().UserName;
                objT_MEMBERSEntity.ModifiedBy = OperatorProvider.Provider.GetCurrent().UserName;
                objT_MEMBERSEntity.CreateOn = DateTime.Now;
                objT_MEMBERSEntity.ModifiedOn = DateTime.Now;
                service.Insert(objT_MEMBERSEntity);
            }
        }
    }
}


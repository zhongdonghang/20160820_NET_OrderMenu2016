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
   public class SeatApp
    {
        private IT_SEATRepository service = new T_SEATRepository();

        /// <summary>
        /// 分页按查询出桌子列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public List<T_SEATEntity> GetList(Pagination pagination, string keyword, int OrgId)
        {
            var expression = ExtLinq.True<T_SEATEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.SeatNo.Contains(keyword));
            }
            expression = expression.And(t => t.OrgID == OrgId);
            return service.FindList(expression, pagination);
        }

        /// <summary>
        /// 按组织机构查询出所有桌子列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public List<T_SEATEntity> GetList(string keyword, int OrgId)
        {
            var expression = ExtLinq.True<T_SEATEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.SeatNo.Contains(keyword));
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
        public T_SEATEntity GetForm(string keyValue)
        {
            return service.FindEntity(int.Parse(keyValue));
        }

        public void DeleteForm(string keyValue)
        {
            int deleteOID = int.Parse(keyValue);
            service.Delete(t => t.OID == deleteOID);
        }

        public void SubmitForm(T_SEATEntity objT_SEATEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))//编辑
            {
                T_SEATEntity oldT_SEATEntity = service.FindEntity(int.Parse(keyValue));
                oldT_SEATEntity.SeatNo = objT_SEATEntity.SeatNo;
                oldT_SEATEntity.PersonNum = objT_SEATEntity.PersonNum;
                oldT_SEATEntity.SaatCategory = objT_SEATEntity.SaatCategory;
                service.Update(oldT_SEATEntity);
            }
            else
            {
                objT_SEATEntity.Status = "0";
                objT_SEATEntity.ParentID = 0;
                objT_SEATEntity.Desc = objT_SEATEntity.SaatCategory;
                service.Insert(objT_SEATEntity);
            }
        }
    }
}

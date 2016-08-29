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
    /// <summary>
    /// 商品类别业务服务类
    /// </summary> 
    public class CategoryApp
    {
        private IT_PRODUCT_CATEORYRepository service = new T_PRODUCT_CATEORYRepository();

        /// <summary>
        /// 分页按查询出类别列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public List<T_PRODUCT_CATEORYEntity> GetList(Pagination pagination, string keyword,int OrgId)
        {
            var expression = ExtLinq.True<T_PRODUCT_CATEORYEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.CName.Contains(keyword));
            }
            expression = expression.And(t => t.OrgID == OrgId);
            return service.FindList(expression, pagination);
        }

        public T_PRODUCT_CATEORYEntity GetForm(string keyValue)
        {
            return service.FindEntity(int.Parse(keyValue));
        }

        public void SubmitForm(T_PRODUCT_CATEORYEntity objT_PRODUCT_CATEORYEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))//编辑
            {
                service.Update(objT_PRODUCT_CATEORYEntity);
               // objT_PRODUCT_CATEORYEntity.Modify(keyValue);
            }
            else
            {
                int OID = service.IQueryable().Max(x => x.OID);
                objT_PRODUCT_CATEORYEntity.OID = OID + 1;
                objT_PRODUCT_CATEORYEntity.ParentID = 0;
                objT_PRODUCT_CATEORYEntity.Code = "code";
                objT_PRODUCT_CATEORYEntity.EName = "ename";
                objT_PRODUCT_CATEORYEntity.ICONID = "0";
                objT_PRODUCT_CATEORYEntity.AllowEdit = 0;
                objT_PRODUCT_CATEORYEntity.DeletionStateCode = 0;
                objT_PRODUCT_CATEORYEntity.Enabled = 0;
                objT_PRODUCT_CATEORYEntity.Description = "暂无";
                objT_PRODUCT_CATEORYEntity.CreateUserId = 0;
                objT_PRODUCT_CATEORYEntity.CreateBy = OperatorProvider.Provider.GetCurrent().UserName;
                objT_PRODUCT_CATEORYEntity.ModifiedBy = OperatorProvider.Provider.GetCurrent().UserName;
                objT_PRODUCT_CATEORYEntity.CreateOn = DateTime.Now;
                objT_PRODUCT_CATEORYEntity.ModifiedOn = DateTime.Now;
                service.Insert(objT_PRODUCT_CATEORYEntity);
            }
        }
    }
}

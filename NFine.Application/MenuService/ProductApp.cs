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
  public  class ProductApp
    {
        private IT_PRODUCTRepository service = new T_PRODUCTRepository();
        private IT_PRODUCT_CATEORYRepository objCategoryservice = new T_PRODUCT_CATEORYRepository();

        /// <summary>
        /// 分页按查询出类别列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public List<T_PRODUCTEntity> GetList(Pagination pagination, string keyword, int OrgId)
        {
            var expression = ExtLinq.True<T_PRODUCTEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.CName.Contains(keyword));
            }
            expression = expression.And(t => t.OrgID == OrgId);
            return service.FindList(expression, pagination);
        }

        public T_PRODUCTEntity GetForm(string keyValue)
        {
            return service.FindEntity(int.Parse(keyValue));
        }

        public void DeleteForm(string keyValue) 
        {
            int deleteOID = int.Parse(keyValue);
            service.Delete(t => t.OID == deleteOID);
        }

        public void SubmitForm(T_PRODUCTEntity objT_PRODUCTEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))//编辑
            {
                T_PRODUCTEntity oldT_PRODUCTEntity = service.FindEntity(int.Parse(keyValue));
                oldT_PRODUCTEntity.CName = objT_PRODUCTEntity.CName;
                oldT_PRODUCTEntity.PCategory = objT_PRODUCTEntity.PCategory;
                oldT_PRODUCTEntity.Price1 = objT_PRODUCTEntity.Price1;
                oldT_PRODUCTEntity.Price2 = objT_PRODUCTEntity.Price2;
                oldT_PRODUCTEntity.SortCode = objT_PRODUCTEntity.SortCode;
                oldT_PRODUCTEntity.PContent = objT_PRODUCTEntity.PContent;
                service.Update(oldT_PRODUCTEntity);
            }
            else
            {
                objT_PRODUCTEntity.Code = Common.CreateNo();
                objT_PRODUCTEntity.PriceString = "--";
                objT_PRODUCTEntity.DeletionStateCode = 0;
                objT_PRODUCTEntity.Enabled = 0;
                objT_PRODUCTEntity.CreateOn = DateTime.Now;
                objT_PRODUCTEntity.ModifiedOn = DateTime.Now;
                objT_PRODUCTEntity.CreateBy = OperatorProvider.Provider.GetCurrent().UserName;
                objT_PRODUCTEntity.ModifiedBy = OperatorProvider.Provider.GetCurrent().UserName;
                objT_PRODUCTEntity.CreateUserId = 0;
                objT_PRODUCTEntity.ModifiedUserId = 0;
                objT_PRODUCTEntity.Description = objCategoryservice.FindEntity(objT_PRODUCTEntity.PCategory).CName;
                service.Insert(objT_PRODUCTEntity);
            }
        }
    }
}

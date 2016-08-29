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
    }
}

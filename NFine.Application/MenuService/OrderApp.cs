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
   public class OrderApp
    {
        private IT_ORDERRepository service = new T_ORDERRepository();
        private IT_ORDER_INFORepository orderInfoService = new T_ORDER_INFORepository();

        public List<T_ORDEREntity> GetList(Pagination pagination, string keyword, int OrgId)
        {
            var expression = ExtLinq.True<T_ORDEREntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.OrderNo.Contains(keyword));
            }
            expression = expression.And(t => t.OrderNo.StartsWith(OrgId.ToString()));
            expression = expression.And(t => t.OrderState == 1);
            return service.FindList(expression, pagination);
        }

    }
}

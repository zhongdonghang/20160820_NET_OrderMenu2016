using NFine.Code;
using NFine.Domain._02_ViewModel;
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
        private IT_ORDER_CHECKOUTRepository checkOutInfoService = new T_ORDER_CHECKOUTRepository();

        /// <summary>
        /// 收银台支付
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <param name="Price1"></param>
        /// <param name="Price2"></param>
        /// <param name="Dec"></param>
        /// <returns></returns>
        public void Pay(string OrderNo,string Price1,string Price2,string Dec)
        {
            //改变原有订单状态
            T_ORDEREntity orderMain = service.FindEntity(t => t.OrderNo == OrderNo);
            orderMain.ModifiedOn = DateTime.Now;
            orderMain.OrderState = 2;
            service.Update(orderMain);
            //添加支付信息
            T_ORDER_CHECKOUTEntity checkInfo = new T_ORDER_CHECKOUTEntity();
            checkInfo.OrderNo = OrderNo;
            checkInfo.Price1 = decimal.Parse(Price1);
            checkInfo.Price2 = decimal.Parse(Price2);
            checkInfo.Dec = Dec;
            checkInfo.OrgID = OperatorProvider.Provider.GetCurrent().OrgId;
            checkInfo.CreateOn = DateTime.Now;
            checkInfo.CreateUserId = 0;
            checkInfo.CreateBy = OperatorProvider.Provider.GetCurrent().UserName;
            checkOutInfoService.Insert(checkInfo);
        }

        /// <summary>
        /// 查询未结账订单
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
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

        public List<T_ORDEREntity> GetAllList(Pagination pagination, string keyword, int OrgId)
        {
            var expression = ExtLinq.True<T_ORDEREntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.OrderNo.Contains(keyword));
            }
            expression = expression.And(t => t.OrderNo.StartsWith(OrgId.ToString()));
          //  expression = expression.And(t => t.OrderState == 1);
            return service.FindList(expression, pagination);
        }



        /// <summary>
        /// 根据主键查询订单的主信息（单条），明细信息（多条），支付信息（单条）
        /// </summary>
        /// <param name="orderOID"></param>
        /// <returns></returns>
        public FullOrder GetOneByKey(string orderOID)
        {
            FullOrder order = new FullOrder();
            T_ORDEREntity orderEntity = service.FindEntity(int.Parse(orderOID));
            List<T_ORDER_INFOEntity> detailList = orderInfoService.FindList("select * from T_ORDER_INFO where OrderNo = '"+ orderEntity.OrderNo + "'");
            T_ORDER_CHECKOUTEntity checkOutInfo = checkOutInfoService.FindEntity(t=>t.OrderNo == orderEntity.OrderNo);
            order.Order = orderEntity;
            order.OrderDetails = detailList;
            order.CheckOutInfo = checkOutInfo;
            return order;
        }

        public void DeleteForm(string keyValue)
        {
            T_ORDEREntity order = service.FindEntity(int.Parse(keyValue));
            order.OrderState = -1;
            order.ModifiedOn = DateTime.Now;
            service.Update(order);
        }

    }
}

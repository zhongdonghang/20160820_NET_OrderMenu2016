using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._02_ViewModel
{
    public class FullOrder
    {
        /// <summary>
        /// 订单主信息
        /// </summary>
        public T_ORDEREntity Order { get; set; }

        /// <summary>
        /// 订单明细信息
        /// </summary>
        public List<T_ORDER_INFOEntity> OrderDetails { get; set; }

        /// <summary>
        /// 订单支付信息
        /// </summary>
        public T_ORDER_CHECKOUTEntity CheckOutInfo { get; set; }

        public decimal Total
        {
            get
            {
                decimal money = 0;

                foreach (var item in OrderDetails)
                {
                    money += item.Price1 * (decimal)item.PNum;
                }

                return money;
            }
        }
    }
}

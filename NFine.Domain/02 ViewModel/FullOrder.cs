using NFine.Code;
using NFine.Domain._03_Entity.MenuBiz;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._02_ViewModel
{
    public class FullOrder
    {
        public string CompanyName
        {
            get
            {
                return OperatorProvider.Provider.GetCurrent().CompanyName;
            }
        }

        public string CompanyPhone
        {
            get
            {
                return OperatorProvider.Provider.GetCurrent().CompanyPhone ;
            }
        }

        public string CompanyAddr
        {
            get
            {
                return OperatorProvider.Provider.GetCurrent().CompanyAddr;
            }
        }

        public string OrderStatusDesc
        {
            get {
                string str = "";
                if (Order.OrderState == 0)
                {
                    str = "未生效";
                }else if (Order.OrderState == 1)
                {
                    str = "未支付";
                }else if (Order.OrderState == 2)
                {
                    str = "已支付";
                }
                return str;
            }
        }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._02_ViewModel
{
    /// <summary>
    /// 首页顶部数据实体
    /// </summary>
    public class HomePageTopViewModel
    {
        public int OID { get; set; }

        public int OrgID { get; set; }

        /// <summary>
        /// 当前在线订单数
        /// </summary>
        public string 当前在线订单数 { get; set; }
        /// <summary>
        /// 当天订单总数
        /// </summary>
        public string 当天订单总数 { get; set; }
        /// <summary>
        /// 当天销售总额
        /// </summary>
        public string 当天销售总额 { get; set; }
        /// <summary>
        /// 当天已收金额
        /// </summary>
        public string 当天已收金额 { get; set; }
        /// <summary>
        /// 当天平均单额
        /// </summary>
        public string 当天平均单额 { get; set; }
    }
}

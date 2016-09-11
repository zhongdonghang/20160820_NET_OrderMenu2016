using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._02_ViewModel
{
    [Serializable]
    public class ApiOrderAndInfosModel
    {
        public List<T_ORDEREntity> order { get; set; }

        public List<T_ORDER_INFOEntity> order_info { get; set; }
    }
}

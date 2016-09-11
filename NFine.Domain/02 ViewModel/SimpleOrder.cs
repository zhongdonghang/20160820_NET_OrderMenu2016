using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._02_ViewModel
{
  public  class SimpleOrder
    {
        public string orderNo { get; set; }
        public DateTime CreateTime { get; set; }
        public string Seat { get; set; }
        public int PeopleNum { get; set; }
        public string MemberName { get; set; }
        public string Dec { get; set; }
        public List<T_ORDER_INFOEntity> orderInfo { get; set; }
    }
}

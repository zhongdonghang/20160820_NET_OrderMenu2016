using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._02_ViewModel
{
   public class ApiSeatsAndEmpsModel
    {
        public List<T_SEATEntity> Seats { get; set; }

        public List<T_MEMBERSEntity> Members { get; set; }
    }
}

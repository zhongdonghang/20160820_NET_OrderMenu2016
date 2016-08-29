using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.MenuBizMapping
{
    public class T_ORDER_CHECKOUTMap: EntityTypeConfiguration<T_ORDER_CHECKOUTEntity>
    {
        public T_ORDER_CHECKOUTMap()
        {
            this.ToTable("T_ORDER_CHECKOUT");
            this.HasKey(t => t.OID);
        }
    }
}

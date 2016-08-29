using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.MenuBizMapping
{
    public class T_ORDER_INFOMap: EntityTypeConfiguration<T_ORDER_INFOEntity>
    {
        public T_ORDER_INFOMap()
        {
            this.ToTable("T_ORDER_INFO");
            this.HasKey(t => t.OID);
        }
    }
}

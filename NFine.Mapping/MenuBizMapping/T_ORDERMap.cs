using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.MenuBizMapping
{
    public class T_ORDERMap: EntityTypeConfiguration<T_ORDEREntity>
    {
        public T_ORDERMap()
        {
            this.ToTable("T_ORDER");
            this.HasKey(t => t.OID);
        }
    }
}

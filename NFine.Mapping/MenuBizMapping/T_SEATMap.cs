using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.MenuBizMapping
{
    public class T_SEATMap: EntityTypeConfiguration<T_SEATEntity>
    {
        public T_SEATMap()
        {
            this.ToTable("T_SEAT");
            this.HasKey(t => t.OID);
        }
            
    }
}

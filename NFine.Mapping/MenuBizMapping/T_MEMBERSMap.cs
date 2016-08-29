using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.MenuBizMapping
{
    public class T_MEMBERSMap: EntityTypeConfiguration<T_MEMBERSEntity>
    {
        public T_MEMBERSMap()
        {
            this.ToTable("T_MEMBERS");
            this.HasKey(t => t.OID);
        }
    }
}

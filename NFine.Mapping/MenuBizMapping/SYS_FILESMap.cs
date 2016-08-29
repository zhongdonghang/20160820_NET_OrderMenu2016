using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.MenuBizMapping
{
    public class SYS_FILESMap: EntityTypeConfiguration<SYS_FILESEntity>
    {
        public SYS_FILESMap()
        {
            this.ToTable("SYS_FILES");
            this.HasKey(t => t.OID);
        }
    }
}

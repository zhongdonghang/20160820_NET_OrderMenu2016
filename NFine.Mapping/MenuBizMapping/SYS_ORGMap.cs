using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using NFine.Domain._03_Entity.MenuBiz;

namespace NFine.Mapping.MenuBizMapping
{
    public class SYS_ORGMap : EntityTypeConfiguration<SYS_ORGEntity>
    {
        public SYS_ORGMap()
        {
            this.ToTable("SYS_ORG");
            this.HasKey(t => t.OID);
        }
    }
}

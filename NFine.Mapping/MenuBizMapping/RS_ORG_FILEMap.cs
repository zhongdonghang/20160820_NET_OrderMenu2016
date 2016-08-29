using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.MenuBizMapping
{
    public class RS_ORG_FILEMap: EntityTypeConfiguration<RS_ORG_FILEEntity>
    {
        public RS_ORG_FILEMap()
        {
            this.ToTable("RS_ORG_FILE");
            this.HasKey(t => t.OID);
        }
    }
}

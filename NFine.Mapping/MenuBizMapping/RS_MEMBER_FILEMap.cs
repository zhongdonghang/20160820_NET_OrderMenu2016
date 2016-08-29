using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.MenuBizMapping
{
    public class RS_MEMBER_FILEMap: EntityTypeConfiguration<RS_MEMBER_FILEEntity>
    {
        public RS_MEMBER_FILEMap()
        {
            this.ToTable("RS_MEMBER_FILE");
            this.HasKey(t => t.OID);
        }
    }
}

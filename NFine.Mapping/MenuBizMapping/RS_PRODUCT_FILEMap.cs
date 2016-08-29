using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.MenuBizMapping
{
    public class RS_PRODUCT_FILEMap: EntityTypeConfiguration<RS_PRODUCT_FILEEntity>
    {
        public RS_PRODUCT_FILEMap()
        {
            this.ToTable("RS_PRODUCT_FILE");
            this.HasKey(t => t.OID);
        }
    }
}

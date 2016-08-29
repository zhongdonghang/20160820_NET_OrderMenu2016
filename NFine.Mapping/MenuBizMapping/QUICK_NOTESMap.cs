using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.MenuBizMapping
{
    public class QUICK_NOTESMap: EntityTypeConfiguration<QUICK_NOTESEntity>
    {
        public QUICK_NOTESMap()
        {
            this.ToTable("QUICK_NOTES");
            this.HasKey(t => t.OID);
        }
    }
}

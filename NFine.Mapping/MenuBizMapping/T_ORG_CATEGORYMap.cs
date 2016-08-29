using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.MenuBizMapping
{
    public class T_ORG_CATEGORYMap: EntityTypeConfiguration<T_ORG_CATEGORYEntity>
    {
        public T_ORG_CATEGORYMap()
        {
            this.ToTable("T_ORG_CATEGORY");
            this.HasKey(t => t.OID);
        }
    }
}

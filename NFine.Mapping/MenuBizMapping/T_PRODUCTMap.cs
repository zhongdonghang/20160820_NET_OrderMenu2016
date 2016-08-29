using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.MenuBizMapping
{
    public class T_PRODUCTMap: EntityTypeConfiguration<T_PRODUCTEntity>
    {
        public T_PRODUCTMap()
        {
            this.ToTable("T_PRODUCT");
            this.HasKey(t => t.OID);
        }
    }
}

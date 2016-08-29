using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.MenuBizMapping
{
    public class T_PRODUCT_CATEORYMap: EntityTypeConfiguration<T_PRODUCT_CATEORYEntity>
    {
        public T_PRODUCT_CATEORYMap()
        {
            this.ToTable("T_PRODUCT_CATEORY");
            this.HasKey(t => t.OID);
        }
    }
}

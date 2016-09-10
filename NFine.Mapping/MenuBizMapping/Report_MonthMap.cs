using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.MenuBizMapping
{
   public class Report_MonthMap: EntityTypeConfiguration<Report_MonthEntity>
    {
        public Report_MonthMap()
        {
            this.ToTable("Report_Month");
            this.HasKey(t => t.OID);
        }
    }
}

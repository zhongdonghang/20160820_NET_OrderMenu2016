using NFine.Domain._03_Entity.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.MenuBizMapping
{
  public  class Report_DayMap: EntityTypeConfiguration<Report_DayEntity>
    {
        public Report_DayMap()
        {
            this.ToTable("Report_Day");
            this.HasKey(t => t.OID);
        }
    }
}

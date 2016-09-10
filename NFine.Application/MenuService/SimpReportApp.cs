using NFine.Code;
using NFine.Domain._03_Entity.MenuBiz;
using NFine.Domain._04_IRepository.MenuBiz;
using NFine.Repository.MenuBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.MenuService
{
   public class SimpReportApp
    {

        private IReport_DayRepository service = new Report_DayRepository();

        /// <summary>
        ///查询日报表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public List<Report_DayEntity> GetList(Pagination pagination, int OrgId)
        {
            var expression = ExtLinq.True<Report_DayEntity>();
            expression = expression.And(t => t.OrgID == OrgId);
            return service.FindList(expression, pagination);
        }

        /// <summary>
        /// 生成指定门店的所有日报表
        /// </summary>
        /// <param name="OrgId"></param>
        public void CreateDayReport(int OrgId)
        {
            //找出当前所有已经结账的

        }

        public void CreateMonthReport(int OrgId)
        {

        }
    }
}

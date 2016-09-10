using NFine.Code;
using NFine.Data.Extensions;
using NFine.Domain._03_Entity.MenuBiz;
using NFine.Domain._04_IRepository.MenuBiz;
using NFine.Repository.MenuBiz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.MenuService
{
   public class SimpReportApp
    {

        private IReport_DayRepository service = new Report_DayRepository();

        private IT_ORDERRepository orderService = new T_ORDERRepository();
        private IT_ORDER_INFORepository orderInfoService = new T_ORDER_INFORepository();
        private IT_ORDER_CHECKOUTRepository checkOutInfoService = new T_ORDER_CHECKOUTRepository();

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
        /// 生成所有门店的所有日报表
        /// </summary>
        public void CreateDayReport(int OrgId)
        {
            //先移除当前所有记录
            service.Delete(t => t.OrgID == OrgId);
            
                string sql = "select CONVERT(varchar(10), CreateOn, 120 ) as CrDate , " +
                    " count(OrderNo) as 订单数量, " +
                    " sum(Price2) as 营业额, " +
                    " max(Price2) as 最高单额, " +
                    " min(Price2) as 最低单额, " +
                    " avg(Price2) as 平均单额 " +
                    " from[dbo].[T_ORDER_CHECKOUT] where OrgID = "+ OrgId + "  " +
                    "  group by  CONVERT(varchar(10), CreateOn, 120)";

                DataTable dt = DbHelper.QueryDataTable(sql);

            List<Report_DayEntity> list = new List<Report_DayEntity>();
            foreach (DataRow  item in dt.Rows)
            {
                Report_DayEntity entity = new Report_DayEntity();
                entity.订单数量 = int.Parse(item["订单数量"].ToString());
                entity.营业额 = decimal.Parse(item["营业额"].ToString());
                entity.最高单额 = decimal.Parse(item["最高单额"].ToString());
                entity.最低单额 = decimal.Parse(item["最低单额"].ToString());
                entity.平均单额 = decimal.Parse(item["平均单额"].ToString());
                entity.OrgID = OrgId;
                entity.CrDate = DateTime.Parse(item["CrDate"].ToString());
                entity.年 = entity.CrDate.Year;
                entity.月 = entity.CrDate.Month;
                entity.日 = entity.CrDate.Day;
                list.Add(entity);
            }
                service.Insert(list);
            
        }

        public void CreateMonthReport(int OrgId)
        {

        }
    }
}

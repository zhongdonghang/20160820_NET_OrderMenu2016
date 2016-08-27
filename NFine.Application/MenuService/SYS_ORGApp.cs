using Newtonsoft.Json;
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
    public class SYS_ORGApp
    {
        private ISYS_ORGRepository service = new SYS_ORGRepository();

        public string GetOrgListForJson(string _pageIndex, string _pageSize,string _cname)
        {
            ReturnPageResult<SYS_ORGEntity> ret = new ReturnPageResult<SYS_ORGEntity>();
            try
            {
                Pagination pagination = new Pagination();
                pagination.rows = int.Parse( _pageSize);
                pagination.page = int.Parse( _pageIndex);
                pagination.sord = "asc";
                pagination.sidx = "OID,FullName";
                var expression = ExtLinq.True<SYS_ORGEntity>();
                List<SYS_ORGEntity> list = service.FindList(expression, pagination);
                ret.Msg = "查询成功";
                ret.Data = list;
                ret.ResultCode = "200";
            }
            catch (Exception ex)
            {
                ret.ResultCode = "-1";
                ret.Msg = "异常:" + ex;
            }

            return JsonConvert.SerializeObject(ret);
        }

        public List<SYS_ORGEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<SYS_ORGEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.ShortName.Contains(keyword));
                expression = expression.Or(t => t.FullName.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }
    }
}

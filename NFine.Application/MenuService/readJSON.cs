using NFine.Data.Extensions;
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
    public class readJSON
    {
        private IT_ORDERRepository orderservice = new T_ORDERRepository();
        private IT_ORDER_INFORepository orderInfoService = new T_ORDER_INFORepository();

        public string readJ(string _json)
        {
            string res = string.Empty;
            string aa = string.Empty;
            string bb = string.Empty;
            string jsontext = "[{ID:'1',Name:'zhangsan',Other:[{Age:'111'}]}]";
            Newtonsoft.Json.Linq.JArray ja = (Newtonsoft.Json.Linq.JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(jsontext);
            foreach (Newtonsoft.Json.Linq.JObject item in ja)
            {
                aa = item["Name"].ToString();
                Newtonsoft.Json.Linq.JArray ja0 = (Newtonsoft.Json.Linq.JArray)item["Other"];
                foreach (Newtonsoft.Json.Linq.JObject item0 in ja0)
                {
                    bb = item0["Age"].ToString();
                }
            }
            return res = aa + "\n" + bb;
        }
        public string readJ2(string _json)
        {
            string res = string.Empty;
            string EmpNo = string.Empty;//员工姓名
            string ProductId = string.Empty;
            string CName = string.Empty;
            string Price1 = string.Empty;
            string Price2 = string.Empty;
            string Num = string.Empty;
            string orderid = string.Empty;
            string PersonNum = string.Empty;
            string SeatNo = string.Empty;
            Newtonsoft.Json.Linq.JArray ja = (Newtonsoft.Json.Linq.JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(_json);
            foreach (Newtonsoft.Json.Linq.JObject item in ja)
            {
                EmpNo = item["EmpNo"].ToString();
                orderid = item["orderid"].ToString();
                PersonNum = item["PersonNum"].ToString();
                SeatNo = item["SeatNo"].ToString();
                Newtonsoft.Json.Linq.JArray ja0 = (Newtonsoft.Json.Linq.JArray)item["items"];
                foreach (Newtonsoft.Json.Linq.JObject item0 in ja0)
                {
                    ProductId = item0["ProductId"].ToString();
                    CName = item0["CName"].ToString();
                    Price1 = item0["Price1"].ToString();
                    Price2 = item0["Price2"].ToString();
                    Num = item0["Num"].ToString();
                }
            }
            return res = EmpNo + "\t" + ProductId + "\t" + CName + "\t" + Price1 + "\t" + Price2 + "\t" + Num + "\t" + orderid + "\t" + PersonNum + "\t" + SeatNo;
        }
        public string readJ3(string _json, string op)
        {
            string re = "提交失败，请检查网络连接！";
            string orderid = string.Empty;//订单号
            string EmpNo = string.Empty;//员工姓名
                                        //T_ORDEREntity bto = new T_ORDEREntity();
                                        //T_ORDER_INFOEntity btoi = new T_ORDER_INFOEntity();
            T_ORDEREntity mto = new T_ORDEREntity();//订单主表
            T_ORDER_INFOEntity mtoi = new T_ORDER_INFOEntity();

            Newtonsoft.Json.Linq.JArray ja = (Newtonsoft.Json.Linq.JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(_json);
            try
            {
                foreach (Newtonsoft.Json.Linq.JObject item in ja)
                {
                    orderid = item["orderid"].ToString();
                    string strWhere = " OrderNo='" + orderid + "'";
                    if (op == "add")
                    {
                        //  mto = bto.GetModel(strWhere);
                        string sql = "select * from T_ORDER where OrderNo = '" + orderid + "'";
                        mto = orderservice.FindList(sql)[0];
                        mto.MemberName = EmpNo = item["EmpNo"].ToString();
                        mto.PeopleNum = Convert.ToInt32(item["PersonNum"].ToString());
                        mto.Seat = item["SeatNo"].ToString();
                        mto.Dec = item["Memo"].ToString();
                        mto.OrderState = 1;
                        mto.ModifiedOn = DateTime.Now;
                        // bto.Update(mto);
                        orderservice.Update(mto);
                        Newtonsoft.Json.Linq.JArray ja0 = (Newtonsoft.Json.Linq.JArray)item["items"];
                        for (int i = 0; i < ja0.Count; i++)
                        {
                            Newtonsoft.Json.Linq.JArray ja1 = (Newtonsoft.Json.Linq.JArray)ja0[i];
                            mtoi.ProductId = ja1[0].ToString();
                            mtoi.ProductCname = ja1[1].ToString();
                            mtoi.Price1 = Convert.ToDecimal(ja1[2].ToString());
                            mtoi.Price2 = Convert.ToDecimal(ja1[3].ToString());
                            mtoi.PNum = float.Parse(ja1[4].ToString());
                            mtoi.MemberName = EmpNo;
                            mtoi.OrderNo = orderid;
                            mtoi.CreateOn = DateTime.Now;
                            mtoi.ModifiedOn = DateTime.Now;
                            orderInfoService.Insert(mtoi);
                          //  btoi.Add(mtoi);
                        }
                    }
                    if (op == "edit")
                    {
                        //  mto = bto.GetModel(strWhere);
                        string sql = "select * from T_ORDER where OrderNo = '" + orderid + "'";
                        mto = orderservice.FindList(sql)[0];
                        mto.MemberName = EmpNo = item["EmpNo"].ToString();
                        mto.PeopleNum = Convert.ToInt32(item["PersonNum"].ToString());
                        mto.Seat = item["SeatNo"].ToString();
                        mto.Dec = item["Memo"].ToString();
                        mto.OrderState = 1;
                        mto.ModifiedOn = DateTime.Now;
                        // bto.Update(mto);
                        orderservice.Update(mto);
                        // btoi.Delete(strWhere);
                        DbHelper.ExecuteSqlCommand(" delete from T_ORDER_INFO where OrderNo = '"+ orderid + "'");
                        Newtonsoft.Json.Linq.JArray ja0 = (Newtonsoft.Json.Linq.JArray)item["items"];
                        for (int i = 0; i < ja0.Count; i++)
                        {
                            Newtonsoft.Json.Linq.JArray ja1 = (Newtonsoft.Json.Linq.JArray)ja0[i];
                            mtoi.ProductId = ja1[0].ToString();
                            mtoi.ProductCname = ja1[1].ToString();
                            mtoi.Price1 = Convert.ToDecimal(ja1[2].ToString());
                            mtoi.Price2 = Convert.ToDecimal(ja1[3].ToString());
                            mtoi.PNum = float.Parse(ja1[4].ToString());
                            mtoi.MemberName = EmpNo;
                            mtoi.OrderNo = orderid;
                            mtoi.CreateOn = DateTime.Now;
                            mtoi.ModifiedOn = DateTime.Now;
                            orderInfoService.Insert(mtoi);
                        }
                    }
                }
                re = "提交成功！";
            }
            catch (Exception ex)
            {
                re = "系统错误：" + ex.ToString();
            }
            return re;
        }
    }
}

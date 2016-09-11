using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._02_ViewModel
{
  public  class SimpleProduct
    {
        public string OID { get; set; }//商品编号
        public string CName { get; set; }//商品名称
        public string Price1 { get; set; }//商品售价
        public string Price2 { get; set; }//商品原价
        public string PIngredients { get; set; }//商品用料
        public string PContent { get; set; }//商品描述
        public string PPractice { get; set; }//商品做法
        public string ImgName { get; set; }//商品图片
    }
}

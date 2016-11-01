using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace NFine.Code
{
   public   class SMSTools
    {
        /// <summary>
        /// 短信发送
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Send(string mobile,string content)
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["url"].ToString();
            string param = "accesskey="+ System.Configuration.ConfigurationManager.AppSettings["accesskey"].ToString() + "&secretkey="+ System.Configuration.ConfigurationManager.AppSettings["secretkey"].ToString() + "&mobile="+ mobile + "&content=" + content + "";
            string strURL =url + '?' + param;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return strValue;
        }
    }
}

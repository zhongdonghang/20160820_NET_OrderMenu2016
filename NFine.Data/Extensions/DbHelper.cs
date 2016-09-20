/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace NFine.Data.Extensions
{
    public class DbHelper
    {
        private static string connstring = ConfigurationManager.ConnectionStrings["NFineDbContext"].ConnectionString;

        /// <summary>
        /// 钟东航添加，执行sql server的表查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable QueryDataTable(string sql)
        {
            DataTable dt = new DataTable();
            using (DbConnection conn = new SqlConnection(connstring))
            {
                SqlDataAdapter ad = new SqlDataAdapter(sql, connstring);
                ad.Fill(dt);
            }
            return dt;
        }

        /// <summary>
        /// 钟东航添加，执行sql server的表查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet QueryDataSet(string sql)
        {
            DataSet ds = new DataSet();
            using (DbConnection conn = new SqlConnection(connstring))
            {
                SqlDataAdapter ad = new SqlDataAdapter(sql, connstring);
                ad.Fill(ds);
            }
            return ds;
        }

        public static int ExecuteSqlCommand(string cmdText)
        {
            using (DbConnection conn = new SqlConnection(connstring))
            {
                DbCommand cmd = new SqlCommand();
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, null);
                return cmd.ExecuteNonQuery();
            }
        }
        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction isOpenTrans, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (isOpenTrans != null)
                cmd.Transaction = isOpenTrans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                cmd.Parameters.AddRange(cmdParms);
            }
        }
    }
}

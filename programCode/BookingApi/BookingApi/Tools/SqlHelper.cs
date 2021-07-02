using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using NLog;

namespace BookingApi.Tools
{
    public class SqlHelper
    {
        /// <summary>
        /// sql链接
        /// </summary>
        private static readonly string conn = DEsecretHelper.Decrypt(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);

        /// <summary>
        /// 日志记录对象
        /// </summary>
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 执行sql语句返回表格（参数化查询）
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public static DataTable execSqlToTable(string strSql, SqlParameter[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapt = new SqlDataAdapter();
            try
            {
                cmd.CommandText = strSql;
                foreach (SqlParameter parm in sqlParameters)
                    cmd.Parameters.Add(parm);
                connection.Open();
                cmd.Connection = connection;
                adapt.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                log.Info("异常语句：" + strSql);
                log.Info("执行sql异常:" + ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// 执行sql语句返回表格（无参数查询）
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataTable execSqlToTable(string strSql)
        {
            return execSqlToTable(strSql, new SqlParameter[] { });
        }

        /// <summary>
        /// 执行sql语句（插入、更新、删除、存储过程、批量语句）（带参数）
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public static int execNoneSelect(string strSql, SqlParameter[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            try
            {
                connection.Open();
                cmd.CommandText = strSql;
                foreach (SqlParameter param in sqlParameters)
                    cmd.Parameters.Add(param);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                log.Info("异常语句：" + strSql);
                log.Info("执行sql异常:" + ex.Message);
                return -1;
            }
        }

        /// <summary>
        ///  执行sql语句（插入、更新、删除、存储过程、批量语句）（无参数）
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static int execNoneSelect(string strSql)
        {
            return execNoneSelect(strSql, new SqlParameter[] { });
        }

        /// <summary>
        /// 大批量数据进表方法
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="destTableName"></param>
        /// <param name="col"></param>
        /// <param name="filter"></param>
        public static void SqlBulkCopy(DataTable dt, string destTableName, int col, string filter)
        {
            try
            {
                System.Data.SqlClient.SqlBulkCopy bcp = new System.Data.SqlClient.SqlBulkCopy(conn);
                bcp.BatchSize = 100;//每次传输的行数    
                bcp.NotifyAfter = 100;//进度提示的行数    
                bcp.DestinationTableName = destTableName;//目标表  
                for (int i = 0; i < col; i++)
                {
                    bcp.ColumnMappings.Add(i, i);
                }
                if (filter == "")
                {
                    bcp.WriteToServer(dt);
                }
                else
                {
                    bcp.WriteToServer(dt.Select(filter));
                }
            }
            catch (Exception ex)
            {
                log.Info("批量导入异常:" + ex.Message);
            }
        }

        public static void SqlBulkCopy(DataTable dt)
        {
            SqlBulkCopy(dt, dt.TableName, dt.Columns.Count, "");
        }
    }
}
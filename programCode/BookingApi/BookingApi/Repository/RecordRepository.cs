using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using BookingApi.IRepository;
using BookingApi.Models;
using BookingApi.Tools;

namespace BookingApi.Repository
{
    public class RecordRepository : RecordIRepository
    {
        /// <summary>
        /// 获取记录列表
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public ApiResultModel GetRecordList(RecordInfoModel record)
        {
            string userName = record.userName;
            string yearMonth = record.yearMonth;
            string strSql = "";
            strSql = @"
                SELECT  r.isInOrOut ,
                        r.ioSourceId ,
                        r.remark ,
                        r.amount ,
                        r.happenTime ,
                        r.accountId
                FROM    dbo.InOrOutRecord r
                        LEFT JOIN dbo.users u ON r.userId = u.id
                WHERE   u.userName = @userName 
                        AND CONVERT(NVARCHAR(6),r.happenTime,10) = @yearMonth";
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@userName", userName);
            sqlParameters[0] = new SqlParameter("@yearMonth", yearMonth);
            DataTable dt = SqlHelper.execSqlToTable(strSql, sqlParameters);
            return new ApiResultModel()
            {
                Status = "2000",
                Data = dt,
                Message = "成功"
            };
        }

        /// <summary>
        /// 获取月份列表
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public ApiResultModel GetMonthList(RecordInfoModel record)
        {
            string userName = record.userName;
            string strSql = "";
            strSql = @"
                SELECT  DISTINCT CONVERT(NVARCHAR(6),r.happenTime,120) AS MONTH ,
                FROM    dbo.InOrOutRecord r
                        LEFT JOIN dbo.users u ON r.userId = u.id
                WHERE   u.userName = @userName";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@userName", userName);
            DataTable dt = SqlHelper.execSqlToTable(strSql, sqlParameters);
            return new ApiResultModel()
            {
                Status = "2000",
                Data = dt,
                Message = "成功"
            };
        }

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public ApiResultModel AddRecord(RecordInfoModel record)
        {
            string happenTime = record.happenTime;
            string remark = record.remark;
            string amount = record.amount;
            string account = record.account;
            string ioSource = record.ioSource;
            string isInOrOut = record.isInOrOut;
            string userName = record.userName;
            string strSql = "";
            if(account == "" || amount == "" || ioSource == "" || isInOrOut == "" || userName == "" || happenTime == "")
            {
                return new ApiResultModel()
                {
                    Status = "5000",
                    Message = "有必填项为空",
                    Data = ""
                };
            }
            strSql = "SELECT COUNT(1) FROM dbo.account WHERE accountNo = @accountNo AND isDeleted = '0'";
            DataTable dt = SqlHelper.execSqlToTable(strSql, new SqlParameter[] { new SqlParameter("@accountNo", account) });
            if (dt.Rows[0][0].ToString() == "0")
            {
                return new ApiResultModel()
                {
                    Status = "5000",
                    Message = "账户不存在",
                    Data = ""
                };
            }
            strSql = "SELECT COUNT(1) FROM dbo.systemData WHERE keyName = @ioSource AND isDeleted = '0' AND className = ''";
            dt = SqlHelper.execSqlToTable(strSql, new SqlParameter[] { new SqlParameter("@ioSource", ioSource) });
            if (dt.Rows[0][0].ToString() == "0")
            {
                return new ApiResultModel()
                {
                    Status = "5000",
                    Message = "来源类型错误",
                    Data = ""
                };
            }
            strSql = @"
                INSERT INTO dbo.InOrOutRecord
                        ( userId ,
                          isInOrOut ,
                          ioSourceId ,
                          remark ,
                          amount ,
                          happenTime ,
                          accountId ,
                          createTime ,
                          createId
                        )
                VALUES  ( (SELECT id FROM dbo.users WHERE userName = @userName) , -- userId - int
                          0 , -- isInOrOut - int
                          (SELECT realValue FROM dbo.systemData WHERE className = '' AND isDeleted = '0' AND keyName = @ioSource) , -- ioSourceId - nchar(100)
                          @remark , -- remark - nchar(1000)
                          @amount , -- amount - decimal
                          @happenTime , -- happenTime - datetime
                          (SELECT id FROM dbo.account WHERE accountNo = @accountNo AND isDeleted = '0') , -- accountId - int
                          GETDATE() , -- createTime - datetime
                         (SELECT id FROM dbo.users WHERE userName = @userName)  -- createId - bigint
                        )";
            SqlHelper.execNoneSelect(strSql, new SqlParameter[] { 
                new SqlParameter("@userName", userName),
                new SqlParameter("@accountNo", account),
                new SqlParameter("@happenTime", happenTime),
                new SqlParameter("@amount", amount),
                new SqlParameter("@remark", remark),
                new SqlParameter("@ioSource", ioSource)
            });
            return new ApiResultModel()
            {
                Status = "2000",
                Message = "添加成功",
                Data = ""
            };
        }
    }
}
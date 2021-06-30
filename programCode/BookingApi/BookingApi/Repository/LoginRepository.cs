using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookingApi.IRepository;
using BookingApi.Models;
using BookingApi.Tools;
using NLog;
using System.Data;
using System.Data.SqlClient;

namespace BookingApi.Repository
{
    public class LoginRepository : LoginIRepository
    {
        /// <summary>
        /// 日志记录对象
        /// </summary>
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        public ApiResultModel Login(UserInfoModel user)
        {
            string userName = user.userName;
            string password = user.password;
            string strSql = "";
            strSql = $@"SELECT COUNT(1) FROM dbo.users WHERE userName = @userName AND password = @password";
            SqlParameter[] sqlParameters = new SqlParameter[2];
            SqlParameter userNameParam = new SqlParameter("@userName", userName);
            SqlParameter passwordParam = new SqlParameter("@password", password);
            sqlParameters[0] = userNameParam;
            sqlParameters[1] = passwordParam;
            DataTable dt = SqlHelper.execSqlToTable(strSql, sqlParameters);
            return new ApiResultModel() { };
        }
    }
}
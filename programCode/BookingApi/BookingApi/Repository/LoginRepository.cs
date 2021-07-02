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

        /// <summary>
        /// 登陆接口
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ApiResultModel Login(UserInfoModel user)
        {
            string userName = user.userName;
            string password = user.password;
            string strSql = "";
            // 根据用户名和密码查询用户表是否有对应数据
            strSql = $@"SELECT COUNT(1) FROM dbo.users WHERE userName = @userName AND password = @password";
            // 参数化查询参数对象
            SqlParameter[] sqlParameters = new SqlParameter[2];
            SqlParameter userNameParam = new SqlParameter("@userName", userName);
            SqlParameter passwordParam = new SqlParameter("@password", password);
            sqlParameters[0] = userNameParam;
            sqlParameters[1] = passwordParam;
            // 执行sql语句
            DataTable dt = SqlHelper.execSqlToTable(strSql, sqlParameters);
            // 返回结果
            ApiResultModel result = new ApiResultModel();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    result.Status = "5000";
                    result.Message = "用户名或密码错误";
                }
                else
                {
                    result.Status = "2000";
                    result.Message = "登陆成功";
                }
            }
            else
            {
                result.Status = "5000";
                result.Message = "用户名或密码错误";
            }
            return result;
        }

        /// <summary>
        /// 注册接口
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ApiResultModel SignUp(UserInfoModel user)
        {
            string userName = user.userName;
            string password = user.password;
            string email = user.email;
            string phone = user.phone;
            string sex = user.sex;
            string age = user.age;
            string realName = user.realName;
            string strSql = "";
            return new ApiResultModel();
        }
    }
}
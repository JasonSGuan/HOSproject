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
            sqlParameters[0] = new SqlParameter("@userName", userName);
            sqlParameters[1] = new SqlParameter("@password", password);
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
            string strSql;
            strSql = @"SELECT COUNT(1) FROM dbo.users WHERE userName = @userName AND isDeleted = 0";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@userName", userName);
            DataTable dt = SqlHelper.execSqlToTable(strSql, sqlParameters);
            // 返回结果
            ApiResultModel result = new ApiResultModel();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() != "0")
                {
                    result.Status = "5000";
                    result.Message = "用户名已存在";
                    return result;
                }
            }
            // 插入语句
            strSql = @"
                INSERT INTO dbo.users
                        ( userName ,
                          password ,
                          realName ,
                          sex ,
                          age ,
                          phoneNumber ,
                          email ,
                          familyId ,
                          createTime ,
                          modifyTime ,
                          pwdModifyTime ,
                          isDeleted
                        )
                VALUES  ( @userName , -- userName - nvarchar(50)
                          @password , -- password - nvarchar(20)
                          @realName , -- realName - nvarchar(50)
                          @sex , -- sex - nvarchar(10)
                          @age , -- age - nvarchar(3)
                          @phoneNumber , -- phoneNumber - nvarchar(20)
                          @email , -- email - nvarchar(100)
                          NULL , -- familyId - int
                          GETDATE() , -- createTime - datetime
                          GETDATE() , -- modifyTime - datetime
                          GETDATE() , -- pwdModifyTime - datetime
                          0  -- isDeleted - bit
                        )";
            // 参数化查询对象
            sqlParameters = new SqlParameter[7];
            sqlParameters[0] = new SqlParameter("@userName", userName);
            sqlParameters[1] = new SqlParameter("@password", password);
            sqlParameters[2] = new SqlParameter("@realName", realName);
            sqlParameters[3] = new SqlParameter("@sex", sex);
            sqlParameters[4] = new SqlParameter("@age", age);
            sqlParameters[5] = new SqlParameter("@phoneNumber", phone);
            sqlParameters[6] = new SqlParameter("@email", email);
            // 执行插入语句
            int x = SqlHelper.execNoneSelect(strSql, sqlParameters);
            if (x == 1)
            {
                result.Status = "2000";
                result.Message = "成功";
            }
            else
            {
                result.Status = "5000";
                result.Message = "失败";
            }
            return result;
        }

        public ApiResultModel IsRepeatedUserName(UserInfoModel user)
        {
            string userName = user.userName;
            string strSql;
            // 查询语句
            strSql = @"SELECT COUNT(1) FROM dbo.users WHERE userName = @userName AND isDeleted = 0";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@userName", userName);
            DataTable dt = SqlHelper.execSqlToTable(strSql, sqlParameters);
            // 返回结果
            ApiResultModel result = new ApiResultModel();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    result.Status = "2000";
                    result.Message = "成功";
                }
                else
                {
                    result.Status = "5000";
                    result.Message = "用户名已存在";
                }
            }
            else
            {
                result.Status = "2000";
                result.Message = "成功";
            }
            return result;
        }
    }
}
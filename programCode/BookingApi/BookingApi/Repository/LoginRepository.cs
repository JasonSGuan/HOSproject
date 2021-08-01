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
using BookingApi.ConfigCs;

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
            strSql = $@"SELECT id FROM dbo.users WHERE userName = @userName AND password = @password";
            // 参数化查询参数对象
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@userName", userName);
            sqlParameters[1] = new SqlParameter("@password", AESEncrypt.Encrypt(password));
            // 执行sql语句
            DataTable dt = SqlHelper.execSqlToTable(strSql, sqlParameters);
            // 返回结果
            ApiResultModel result = new ApiResultModel();
            if (dt.Rows.Count > 0)
            {
                result.Status = "2000";
                result.Data = "\"id\":\"" + dt.Rows[0][0].ToString() + "\"";
                result.Message = "登陆成功";
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
            sqlParameters[1] = new SqlParameter("@password", AESEncrypt.Encrypt(password));
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

        /// <summary>
        /// 判断用户名是否重复
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ApiResultModel ResetPassword(UserInfoModel user)
        {
            string userName = user.userName;
            string email = user.email;
            string strSql = "";
            // 查询用户名邮箱是否正确
            strSql = "select count(1) from users where userName = @userName and email = @email";
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@userName", userName);
            sqlParameter[1] = new SqlParameter("@email", email);
            DataTable dtCount = SqlHelper.execSqlToTable(strSql, sqlParameter);
            if (dtCount.Rows[0][0].ToString() == "0")
            {
                return new ApiResultModel()
                {
                    Status = "5000",
                    Message = "用户名或邮箱错误",
                    Data = null
                };
            }
            // 生成随机新密码
            string password = GetRandomString(8);
            // 更新密码
            strSql = "update users set password = @password where userName = @userName";
            sqlParameter[0] = new SqlParameter("@userName", userName);
            sqlParameter[0] = new SqlParameter("@password", password);
            SqlHelper.execNoneSelect(strSql, sqlParameter);
            // 发送邮件
            ManagerEmail.EMail.SendEmail(email, "", "", "重置密码", "您的新密码是：" + password + "请妥善保管，并重新登陆后修改密码！", "", "系统", EmailConfig.email, EmailConfig.password, EmailConfig.emailSmtp, EmailConfig.port, "", true);
            return new ApiResultModel()
            {
                Status = "2000",
                Message = "新密码已发送至邮箱",
                Data = null
            };
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ApiResultModel ChangePassword(UserInfoModel user)
        {
            string userName = user.userName;
            string password = user.password;
            string newPassword = user.newPassword;
            string strSql = "";
            // 判断用户名和原密码是否正确
            strSql = "select count(1) from users where userName = @userName and password = @password";
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@userName", userName);
            sqlParameter[1] = new SqlParameter("@password", AESEncrypt.Encrypt(password));
            DataTable dtCount = SqlHelper.execSqlToTable(strSql);
            if (dtCount.Rows[0][0].ToString() == "0")
            {
                return new ApiResultModel()
                {
                    Status = "5000",
                    Message = "用户名或原密码错误错误",
                    Data = null
                };
            }
            // 更新密码
            strSql = "update users set password = @password where userName = @userName";
            sqlParameter[0] = new SqlParameter("@userName", userName);
            sqlParameter[0] = new SqlParameter("@password", AESEncrypt.Encrypt(newPassword));
            SqlHelper.execNoneSelect(strSql, sqlParameter);
            return new ApiResultModel()
            {
                Status = "2000",
                Message = "修改成功",
                Data = null
            };
        }

        /// <summary>
        /// 生成随机数的种子
        /// </summary>
        /// <returns></returns>
        private static int getNewSeed()
        {
            byte[] rndBytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(rndBytes);
            return BitConverter.ToInt32(rndBytes, 0);
        }
        
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        static public string GetRandomString(int len)
        {
            string s = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string reValue = string.Empty;
            Random rnd = new Random(getNewSeed());
            while (reValue.Length < len)
            {
                string s1 = s[rnd.Next(0, s.Length)].ToString();
                if (reValue.IndexOf(s1) == -1) reValue += s1;
            }
            return reValue;
        }
    }
}
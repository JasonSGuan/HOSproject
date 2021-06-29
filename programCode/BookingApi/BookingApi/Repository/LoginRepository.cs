using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookingApi.IRepository;
using BookingApi.Models;
using NLog;

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
            return new ApiResultModel() { };

        }
    }
}
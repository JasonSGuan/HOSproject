using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApi.Models;

namespace BookingApi.IRepository
{
    public interface LoginIRepository
    {
        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="user">登录信息</param>
        /// <returns></returns>
        ApiResultModel Login(UserInfoModel user);
    }
}

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

        /// <summary>
        /// 注册接口
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ApiResultModel SignUp(UserInfoModel user);

        /// <summary>
        /// 判断用户名是否重复
        /// </summary>
        /// <param name="useer"></param>
        /// <returns></returns>
        ApiResultModel IsRepeatedUserName(UserInfoModel user);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ApiResultModel ResetPassword(UserInfoModel user);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ApiResultModel ChangePassword(UserInfoModel user);
    }
}

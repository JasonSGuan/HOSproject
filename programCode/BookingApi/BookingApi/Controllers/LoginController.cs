using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookingApi.IRepository;
using BookingApi.Repository;
using BookingApi.Models;
using BookingApi.Filters;

namespace BookingApi.Controllers
{
    [RoutePrefix("api/Login")]
    [LogAction]
    public class LoginController : ApiController
    {
        public LoginIRepository login { get; private set; }
        public LoginController(LoginRepository login)
        {
            this.login = login;
        }

        [Route("test")]
        [HttpGet]
        public string test()
        {
            return "1";
        }

        /// <summary>
        /// 登陆接口
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        public ApiResultModel Login(UserInfoModel user)
        {
            return login.Login(user);
        }

        /// <summary>
        /// 注册接口
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("SignUp")]
        [HttpPost]
        public ApiResultModel SignUp(UserInfoModel user)
        {
            return login.SignUp(user);
        }

        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("Repeated")]
        [HttpPost]
        public ApiResultModel IsRepeatedUserName(UserInfoModel user)
        {
            return login.IsRepeatedUserName(user);
        }
    }
}
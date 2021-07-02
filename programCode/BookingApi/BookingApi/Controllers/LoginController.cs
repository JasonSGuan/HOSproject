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
        public LoginIRepository Customer { get; private set; }
        public LoginController(LoginRepository customer)
        {
            this.Customer = customer;
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
            return Customer.Login(user);
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
            return Customer.SignUp(user);
        }
    }
}
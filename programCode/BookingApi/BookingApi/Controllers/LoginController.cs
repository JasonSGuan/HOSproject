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
    [RoutePrefix("api/Customer")]
    [LogAction]
    public class LoginController : ApiController
    {
        public LoginIRepository Customer { get; private set; }
        public LoginController(LoginRepository customer)
        {
            this.Customer = customer;
        }

        [Route("Login")]
        [HttpPost]
        public ApiResultModel Login(UserInfoModel user)
        {
            return Customer.Login(user);
        }
    }
}
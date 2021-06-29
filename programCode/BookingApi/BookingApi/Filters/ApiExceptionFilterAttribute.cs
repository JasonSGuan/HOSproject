using BookingApi.Common;
using BookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;

namespace BookingApi.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            // 记录错误日志
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new AppLog());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Error(actionExecutedContext.Request, "Controller : " + actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine + "Action : " + actionExecutedContext.ActionContext.ActionDescriptor.ActionName, actionExecutedContext.Exception);

            base.OnException(actionExecutedContext);

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK,
                new ApiResultModel()
                {
                    Status = "4000",
                    Data = null,
                    Message = actionExecutedContext.Exception.Message
                });
        }
    }
}
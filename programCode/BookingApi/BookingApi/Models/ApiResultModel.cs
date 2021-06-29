using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingApi.Models
{
    /// <summary>
    /// 接口返回对象
    /// </summary>
    public class ApiResultModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public string Status;
        /// <summary>
        /// 数据
        /// </summary>
        public object Data;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message;
    }
}
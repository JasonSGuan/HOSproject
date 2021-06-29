using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingApi.Common
{
    public class ApiException:Exception
    {
        public string ErrorCode { get; set; }

        public ApiException(Exception inner, string errorCode)
            : base(inner.Message, new Exception(errorCode))
        {
        }
    }
}
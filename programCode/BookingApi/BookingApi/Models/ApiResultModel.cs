using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingApi.Models
{
    public class ApiResultModel
    {
        public string Status;
        public object Data;
        public string Message;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookingApi.Models;
using BookingApi.IRepository;
using BookingApi.Repository;

namespace BookingApi.Controllers
{
    public class RecordController : ApiController
    {
        public RecordIRepository record { get; private set; }
        public RecordController(RecordRepository record)
        {
            this.record = record;
        }

        public ApiResultModel GetRecordList(RecordInfoModel recordInfo)
        {
            return record.GetRecordList(recordInfo);
        }
    }
}

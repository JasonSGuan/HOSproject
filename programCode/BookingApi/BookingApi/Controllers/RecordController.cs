using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookingApi.Models;
using BookingApi.IRepository;
using BookingApi.Repository;
using BookingApi.Filters;

namespace BookingApi.Controllers
{
    [RoutePrefix("api/Login")]
    [LogAction]
    public class RecordController : ApiController
    {
        public RecordIRepository record { get; private set; }
        public RecordController(RecordRepository record)
        {
            this.record = record;
        }

        [Route("GetRecordList")]
        [HttpGet]
        public ApiResultModel GetRecordList(RecordInfoModel recordInfo)
        {
            return record.GetRecordList(recordInfo);
        }

        [Route("GetMonthList")]
        [HttpGet]
        public ApiResultModel GetMonthList(RecordInfoModel recordInfo)
        {
            return record.GetMonthList(recordInfo);
        }

        [Route("AddRecord")]
        [HttpPost]
        public ApiResultModel AddRecord(RecordInfoModel recordInfo)
        {
            return record.AddRecord(recordInfo);
        }
    }
}

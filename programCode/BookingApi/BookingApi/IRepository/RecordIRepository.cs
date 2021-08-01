using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookingApi.Models;

namespace BookingApi.IRepository
{
    public interface RecordIRepository
    {
        ApiResultModel GetRecordList(RecordInfoModel record);
    }
}
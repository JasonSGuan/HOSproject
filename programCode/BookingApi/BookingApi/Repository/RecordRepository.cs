using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookingApi.IRepository;
using BookingApi.Models;
using BookingApi.Tools;

namespace BookingApi.Repository
{
    public class RecordRepository : RecordIRepository
    {
        public ApiResultModel GetRecordList(RecordInfoModel record)
        {
            string userName = record.userName;
            string strSql = "";
            strSql = @"
                SELECT  r.isInOrOut ,
                        r.ioSourceId ,
                        r.remark ,
                        r.amount ,
                        r.happenTime ,
                        r.accountId
                FROM    dbo.InOrOutRecord r
                        LEFT JOIN dbo.users u ON r.userId = u.id
                WHERE   u.userName = @userName ";

        }
    }
}
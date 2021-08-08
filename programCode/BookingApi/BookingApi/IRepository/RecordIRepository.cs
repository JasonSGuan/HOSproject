using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookingApi.Models;

namespace BookingApi.IRepository
{
    public interface RecordIRepository
    {
        /// <summary>
        /// 获取记录列表
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        ApiResultModel GetRecordList(RecordInfoModel record);

        /// <summary>
        /// 获取月份列表
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        ApiResultModel GetMonthList(RecordInfoModel record);

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        ApiResultModel AddRecord(RecordInfoModel record);
    }
}
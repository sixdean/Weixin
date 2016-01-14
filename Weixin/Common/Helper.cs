using System;
using Weixin.Model;
using Weixin.Model.Common;

namespace Weixin.Common
{
    public static class Helper
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static ErrorJsonResult GetExecuteResult(string url, string postData = null)
        {
            var result = new ErrorJsonResult();
            try
            {
                result = postData != null ? JsonHelper<ErrorJsonResult>.ConvertJson(url, postData) : JsonHelper<ErrorJsonResult>.ConvertJson(url);
            }
            catch (Exception e)
            {
                TextLogHelper.WriteLog("GetExecuteResult:" + e.Message);
            }

            return result;
        }
    }
}

using System;
using Newtonsoft.Json;

namespace Weixin.BLL.Common
{
    public static class ExtendHelper
    {
        public static void Log(this string str)
        {
            TextLogHelper.WriteLog(str);
        }

        /// <summary>
        /// 把对象为json字符串
        /// </summary>
        /// <param name="obj">待序列号对象</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
        }


        /// <summary>
        /// 转换为整数时间类型
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int DateTimeToInt(this DateTime date)
        {
            return Convert.ToInt32((date.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
        }
    }
}
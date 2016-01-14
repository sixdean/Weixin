using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Weixin.Model.Common;

namespace Weixin.Common
{
    public static class Common
    {
        /// <summary>
        ///     获取AccessToken并加入到cache
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static string GetAccessToken(string appid, string secret)
        {
            var access_token = "";
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret);
            var result = HttpHelper.GetResponse<AccessToken>(url);
            access_token = result.access_token;
            ObjectCache cache = MemoryCache.Default;
            cache.Add("AccessToken", access_token, DateTime.Now.AddSeconds(result.expires_in - 200));
            return access_token;
        }


        /// <summary>
        /// 返回easyui datagrid需要个json格式  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetDatagridJsonString<T>(List<T> list)
        {
            var timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            var obj = new { total = list.Count, rows = list };
            return JsonConvert.SerializeObject(obj, Formatting.Indented, timeFormat);
        }
    }
}
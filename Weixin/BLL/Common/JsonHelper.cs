using Newtonsoft.Json;
using Weixin.Model.Common;

namespace Weixin.BLL.Common
{
    /// <summary>
    /// Json字符串操作辅助类
    /// </summary>
    public class JsonHelper<T> where T : class, new()
    {
        /// <summary>
        /// 检查返回的记录，如果返回没有错误，或者结果提示成功，则不抛出异常
        /// </summary>
        /// <param name="content">返回的结果</param>
        /// <returns></returns>
        private static bool VerifyErrorCode(string content)
        {
            if (content.Contains("errcode"))
            {
                ErrorJsonResult errorResult = JsonConvert.DeserializeObject<ErrorJsonResult>(content);
                //非成功操作才记录异常，因为有些操作是返回正常的结果（{"errcode": 0, "errmsg": "ok"}）
                //if (errorResult != null && errorResult.errcode != ReturnCode.请求成功)
                //{
                //    string error = string.Format("微信请求发生错误！错误代码：{0}，说明：{1}", (int)errorResult.errcode, errorResult.errmsg);
                //    //LogTextHelper.Error(errorResult);

                //    //throw new WeixinException(error);//抛出错误
                //}
            }
            return true;
        }

        /// <summary>
        /// 通过url地址get到json字符串并将字符串转换为具体对象
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T ConvertJson(string url)
        {
            string content = HttpHelper.GetResponse(url);
            //VerifyErrorCode(content);
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }


        /// <summary>
        /// post数据并将返回json字符串转换为具体对象
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="postData">post提交的数据</param>
        /// <returns></returns>
        public static T ConvertJson(string url, string postData)
        {
            string content = HttpHelper.PostResponse(url, postData);
            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }
    }
}
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Weixin.Common
{
    public class HttpHelper
    {
        public static string PostResponse(string url, string postData)
        {
            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var httpClient = new HttpClient();
            using (var response = httpClient.PostAsync(url, httpContent).Result)
            {
                return response.IsSuccessStatusCode ? response.Content.ReadAsStringAsync().Result : null;
            }
        }

        public static T PostResponse<T>(string url, string postData)
            where T : class, new()
        {
            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var httpClient = new HttpClient();
            var response = httpClient.PostAsync(url, httpContent).Result;
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result) : default(T);
        }

        public static string GetResponse(string url)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = httpClient.GetAsync(url).Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsStringAsync().Result : null;
        }

        public static T GetResponse<T>(string url)
            where T : class, new()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = httpClient.GetAsync(url).Result;
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result) : default(T);
        }
        #region 另外一种写法
        //    public static T PostResponse<T>(string postUrl, string postData)
        //{
        //    var data = Encoding.UTF8.GetBytes(postData);
        //    try
        //    {
        //        // 设置参数
        //        var request = WebRequest.Create(postUrl) as HttpWebRequest;
        //        var cookieContainer = new CookieContainer();
        //        if (request != null)
        //        {
        //            request.CookieContainer = cookieContainer;
        //            request.AllowAutoRedirect = true;
        //            request.Method = "POST";
        //            request.ContentType = "application/x-www-form-urlencoded";
        //            request.ContentLength = data.Length;
        //            var outstream = request.GetRequestStream();
        //            outstream.Write(data, 0, data.Length);
        //            outstream.Close();
        //            //发送请求并获取相应回应数据
        //            var response = request.GetResponse() as HttpWebResponse;
        //            //直到request.GetResponse()程序才开始向目标网页发送Post请求
        //            if (response != null)
        //            {
        //                var instream = response.GetResponseStream();
        //                if (instream != null)
        //                {
        //                    var sr = new StreamReader(instream, Encoding.UTF8);
        //                    //返回结果网页（html）代码
        //                    var content = sr.ReadToEnd();
        //                    return JsonConvert.DeserializeObject<T>(content);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TextLogHelper.WriteLog("Post数据报错:" + ex.Message + ".postUrl:" + postUrl + ".postData:" + postData);
        //    }
        //    return default(T);
        //}

        //public static string PostResponse(string postUrl, string postData)
        //{
        //    var data = Encoding.UTF8.GetBytes(postData);
        //    try
        //    {
        //        // 设置参数
        //        var request = WebRequest.Create(postUrl) as HttpWebRequest;
        //        var cookieContainer = new CookieContainer();
        //        if (request != null)
        //        {
        //            request.CookieContainer = cookieContainer;
        //            request.AllowAutoRedirect = true;
        //            request.Method = "POST";
        //            request.ContentType = "application/x-www-form-urlencoded";
        //            request.ContentLength = data.Length;
        //            var outstream = request.GetRequestStream();
        //            outstream.Write(data, 0, data.Length);
        //            outstream.Close();
        //            //发送请求并获取相应回应数据
        //            var response = request.GetResponse() as HttpWebResponse;
        //            //直到request.GetResponse()程序才开始向目标网页发送Post请求
        //            if (response != null)
        //            {
        //                var instream = response.GetResponseStream();
        //                if (instream != null)
        //                {
        //                    var sr = new StreamReader(instream, Encoding.UTF8);
        //                    //返回结果网页（html）代码
        //                    var content = sr.ReadToEnd();
        //                    return content;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TextLogHelper.WriteLog("Post数据报错:" + ex.Message + ".postUrl:" + postUrl + ".postData:" + postData);
        //    }
        //    return null;
        //}

        //public static T GetResponse<T>(string getUrl) where T : new()
        //{
        //    try
        //    {
        //        var request = WebRequest.Create(getUrl) as HttpWebRequest;
        //        var cookieContainer = new CookieContainer();
        //        if (request != null)
        //        {
        //            request.CookieContainer = cookieContainer;
        //            request.AllowAutoRedirect = true;
        //            request.Method = "GET";
        //            request.ContentType = "application/x-www-form-urlencoded";
        //            //发送请求并获取相应回应数据
        //            var response = request.GetResponse() as HttpWebResponse;
        //            //直到request.GetResponse()程序才开始向目标网页发送Post请求
        //            if (response != null)
        //            {
        //                var instream = response.GetResponseStream();
        //                if (instream != null)
        //                {
        //                    var sr = new StreamReader(instream, Encoding.UTF8);
        //                    //返回结果网页（html）代码
        //                    var content = sr.ReadToEnd();
        //                    return JsonConvert.DeserializeObject<T>(content);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TextLogHelper.WriteLog("get数据报错:" + ex.Message + ".getUrl:" + getUrl);
        //    }
        //    return default(T);
        //}

        //public static string GetResponse(string getUrl)
        //{
        //    try
        //    {
        //        var request = WebRequest.Create(getUrl) as HttpWebRequest;
        //        var cookieContainer = new CookieContainer();
        //        if (request != null)
        //        {
        //            request.CookieContainer = cookieContainer;
        //            request.AllowAutoRedirect = true;
        //            request.Method = "GET";
        //            request.ContentType = "application/x-www-form-urlencoded";
        //            //发送请求并获取相应回应数据
        //            var response = request.GetResponse() as HttpWebResponse;
        //            //直到request.GetResponse()程序才开始向目标网页发送Post请求
        //            if (response != null)
        //            {
        //                var instream = response.GetResponseStream();
        //                if (instream != null)
        //                {
        //                    var sr = new StreamReader(instream, Encoding.UTF8);
        //                    //返回结果网页（html）代码
        //                    var content = sr.ReadToEnd();
        //                    return content;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TextLogHelper.WriteLog("get数据报错:" + ex.Message + ".getUrl:" + getUrl);
        //    }
        //    return null;
        //}
        #endregion

    }
}
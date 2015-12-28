using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Weixin.Common;
using Weixin.IWeixin;
using Weixin.Model;
using Weixin.Model.Response;
using Weixin.Weixin;

namespace ConTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var url = "https://api.weixin.qq.com/cgi-bin/menu/get?access_token=IUH4zWNd8KXdZpdbRN075OsWxFPGaO3FvbUSFoPEmUiTtjSO8RWk44Cun_u_yQBGQyup7ZF-U01QxrahbjRT6TJHAfhmAEJobCCoSXRlD9oHXGdACAWMV ";

           
           Console.WriteLine(HttpHelper.GetResponse(url));
           Console.WriteLine(HttpHelper.GetResponses(url));
            //var postdata=@"{"menu":{"button":[{"type":"click","name":"六六一","key":"V1001_TODAY_MUSIC","sub_button":[]},{"type":"click","name":"歌手简介","key":"V1001_TODAY_SINGER","sub_button":[]},{"name":"菜单","sub_button":[{"type":"view","name":"搜索","url":"http://www.soso.com/","sub_button":[]},{"type":"view","name":"视频","url":"http://v.qq.com/","sub_button":[]},{"type":"click","name":"赞一下我们","key":"V1001_GOOD","sub_button":[]}]}]}}";
            Console.WriteLine();

            //var s=@"{"button":[{"type":"click","name":"六六一","key":"V1001_TODAY_MUSIC","sub_button":[]},{"type":"click","name":"歌手简介","key":"V1001_TODAY_SINGER","sub_button":[]},{"name":"菜单","sub_button":[{"type":"view","name":"搜索","url":"http","sub_button":[]},{"type":"view","name":"视频","url":"ht","sub_button":[]},{"type":"click","name":"赞一下我们","key":"V1001_GOOD","sub_button
            //":[]}]}]}";
            //   var s= JsonConvert.DeserializeObject<MenuInfo>(s);

            Console.ReadLine();

        }

        public static string GetPage(string posturl)
        {
            Encoding encoding = Encoding.UTF8;
            // 准备请求...
            try
            {
                // 设置参数
                var request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                //发送请求并获取相应回应数据
                var response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                var instream = response.GetResponseStream();
                var sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                var s = JsonConvert.DeserializeObject<MenuInfo>(content);
                Console.WriteLine(content);
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }
        public static void requesttext()
        {
            var posstr =
               "<xml><URL><![CDATA[http://wang494014418.6655.la/Ashx/weixin.ashx]]></URL><ToUserName><![CDATA[gh_2461d20dda43]]></ToUserName><FromUserName><![CDATA[o9WULuDOA1s5S9dagWTvLpeit4aY]]></FromUserName><CreateTime>1451140425</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[这是个测试]]></Content><MsgId>123456765</MsgId></xml>";

            XmlSerializer xlSerializer = new XmlSerializer(typeof(BaseMessage));
            StreamReader reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(posstr)), Encoding.UTF8);
            BaseMessage inof = xlSerializer.Deserialize(reader) as BaseMessage;
            BaseMessage info = XmlSerializerHelper.XmlToObject<BaseMessage>(posstr);

            var s = XmlSerializerHelper.ObjectToXml(info);
            var ss = XmlSerializerHelper.ObjectToXml<BaseMessage>(info);
        }

        public static void responsetest()
        {
            try
            {


                var posstr =
                  "<xml><URL><![CDATA[http://wang494014418.6655.la/Ashx/weixin.ashx]]></URL><ToUserName><![CDATA[gh_2461d20dda43]]></ToUserName><FromUserName><![CDATA[o9WULuDOA1s5S9dagWTvLpeit4aY]]></FromUserName><CreateTime>1451140425</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[这是个测试]]></Content><MsgId>123456765</MsgId></xml>";
                IWeixinAction weixinAction = new WeixinAction();
                BaseMessage info = XmlSerializerHelper.XmlToObject<BaseMessage>(posstr);

                string responseContent = weixinAction.Handle(posstr);
                ResponseText repResponseText = new ResponseText(info);
                repResponseText.Content = responseContent;
                repResponseText.ToXml();
            }
            catch (Exception e)
            {

                var s = e.Message;
            }
        }
    }
}

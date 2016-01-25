using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using Newtonsoft.Json;
using WeatherApi;
using Weixin.BLL.Common;
using Weixin.BLL.Weixin;
using Weixin.IWeixin;
using Weixin.Model.Common;
using Weixin.Model.Response;

namespace ConTest
{
    internal class Program
    {

        delegate string MyDelegate(string name);

        static void Main(string[] args)
        {
            try
            {
                var obj = WeatherApi.Weather.GetWeather("beijing");
                obj = obj.Replace("HeWeather data service 3.0", "HeWeatherList");
                var ss = JsonConvert.DeserializeObject(obj, typeof(WeatherModel.HeWeatherResult)) as WeatherModel.HeWeatherResult;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }

            Console.ReadKey();
        }

        static string Hello(string name)
        {
            ThreadMessage("Async Thread");
            Thread.Sleep(2000);             //模拟异步操作
            return "\nHello " + name;
        }

        static void Completed(IAsyncResult result)
        {
            ThreadMessage("Async Completed");

            //获取委托对象，调用EndInvoke方法获取运行结果
            AsyncResult _result = (AsyncResult)result;
            Console.WriteLine(_result.AsyncDelegate.ToString());
            Console.WriteLine(_result.IsCompleted);
            Console.WriteLine(_result.EndInvokeCalled);
            MyDelegate myDelegate = (MyDelegate)_result.AsyncDelegate;
            string data = myDelegate.EndInvoke(_result);
            var s = _result.AsyncState;
            Console.WriteLine(s);
            Console.WriteLine(data);
        }

        static void ThreadMessage(string data)
        {
            string message = string.Format("{0}\n  ThreadId is:{1}",
                   data, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(message);
        }
        public class Message
        {
            public void ShowMessage()
            {
                string message = string.Format("Async threadId is :{0}",
                                                Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine(message);

                for (int n = 0; n < 10; n++)
                {
                    Thread.Sleep(300);
                    Console.WriteLine("The number is:" + n.ToString());
                }
            }
        }



        public static string GetPage(string posturl)
        {
            var encoding = Encoding.UTF8;
            // 准备请求...
            try
            {
                // 设置参数
                var request = WebRequest.Create(posturl) as HttpWebRequest;
                var cookieContainer = new CookieContainer();
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
                var content = sr.ReadToEnd();
                var err = string.Empty;
                var s = JsonConvert.DeserializeObject<MenuInfo>(content);
                Console.WriteLine(content);
                return content;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                return string.Empty;
            }
        }

        public static void requesttext()
        {
            var posstr =
                "<xml><URL><![CDATA[http://wang494014418.6655.la/Ashx/weixin.ashx]]></URL><ToUserName><![CDATA[gh_2461d20dda43]]></ToUserName><FromUserName><![CDATA[o9WULuDOA1s5S9dagWTvLpeit4aY]]></FromUserName><CreateTime>1451140425</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[这是个测试]]></Content><MsgId>123456765</MsgId></xml>";

            var xlSerializer = new XmlSerializer(typeof(BaseMessage));
            var reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(posstr)), Encoding.UTF8);
            var inof = xlSerializer.Deserialize(reader) as BaseMessage;
            var info = XmlSerializerHelper.XmlToObject<BaseMessage>(posstr);

            var s = XmlSerializerHelper.ObjectToXml(info);
            var ss = XmlSerializerHelper.ObjectToXml(info);
        }

        public static void responsetest()
        {
            try
            {
                var posstr =
                    "<xml><URL><![CDATA[http://wang494014418.6655.la/Ashx/weixin.ashx]]></URL><ToUserName><![CDATA[gh_2461d20dda43]]></ToUserName><FromUserName><![CDATA[o9WULuDOA1s5S9dagWTvLpeit4aY]]></FromUserName><CreateTime>1451140425</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[这是个测试]]></Content><MsgId>123456765</MsgId></xml>";
                IWeixinAction weixinAction = new WeixinAction();
                var info = XmlSerializerHelper.XmlToObject<BaseMessage>(posstr);

                var responseContent = weixinAction.Handle(posstr);
                var repResponseText = new ResponseText(info);
                repResponseText.Content = responseContent;
                repResponseText.ToXml();
            }
            catch (Exception e)
            {
                var s = e.Message;
            }
        }
    }

    internal class Test
    {
        public Test()
        {
            ID = "666";
        }

        public Test(string s)
        {
            ID = s;
        }

        public string ID { get; set; }
    }

    internal class Test1
    {
        public Test1()
        {
            ID = "666";
        }

        public Test1(string s)
        {
            ID = s;
        }

        public string ID { get; set; }
    }
}
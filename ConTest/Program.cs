using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Weixin.BLL.Common;
using Weixin.BLL.Weixin;
using Weixin.IWeixin;
using Weixin.Model.Common;
using Weixin.Model.Response;

namespace ConTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var date = DateTime.Now;


            //var token = "QzIvNahpmUbfRotF43xuZdUnNEnERBtTbCuo29ouxVdRn5tZv4JRYoPVDUKvJz6EJ9VbJ-ANbopSKdg3Vyy7jZpBQthzvVAL7H67RAqwyOkYTJaAGAFAQ";
            //var url = "https://api.weixin.qq.com/cgi-bin/menu/get?access_token=" + token;
            //var menu = HttpHelper.GetResponse<MenuListJson>(url);
            //var mm = HttpHelper.GetResponse(url);
            //var s = JsonConvert.SerializeObject(menu);
            //Console.WriteLine(s);
            //var mm=new MenuInfo("buttonname",ButtonType.click,"sss",null);

            //            var xml = @"<xml><ToUserName><![CDATA[gh_2461d20dda43]]></ToUserName>
            //<FromUserName><![CDATA[o9WULuDOA1s5S9dagWTvLpeit4aY]]></FromUserName>
            //<CreateTime>1451401800</CreateTime>
            //<MsgType><![CDATA[text]]></MsgType>
            //<Content><![CDATA[1234566]]></Content>
            //<MsgId>6233723264757904295</MsgId>
            //</xml>";
            //            BaseMessage bm = new BaseMessage();
            //            //bm.MsgType = "type";
            //            //bm.ToUserName = "me";
            //            //bm.FromUserName = "you";
            //            var p = new object[] { bm };

            //            RequestEvent evEvent = new RequestEvent(bm);
            //            //Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
            //            Assembly assembly = Assembly.Load("Weixin");


            //            Type type = assembly.GetType("Weixin.Model.Request.RequestText");
            //            var mySerializer = new XmlSerializer(type);
            //            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            //            {
            //                var tt = mySerializer.Deserialize(stream);
            //            }
            //            object obj = assembly.CreateInstance("Weixin.Model.Request.RequestEvent", true, BindingFlags.Default, null, p, null, null); //类的完全限定名（即包括命名空间）
            //            string n = "grayworm";
            //            Type t = n.GetType();
            //            foreach (MemberInfo mi in t.GetMembers())
            //            {
            //                Console.WriteLine("{0}      {1}", mi.MemberType, mi.Name);
            //            }
            var s = Common.GetAccessToken("wx2647e739be267f22", "d4624c36b6795d1d99dcf0547af5443d");

            Console.ReadKey();
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

            var xlSerializer = new XmlSerializer(typeof (BaseMessage));
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
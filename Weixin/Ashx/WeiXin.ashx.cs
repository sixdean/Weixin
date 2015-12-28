﻿using System;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls;
using Weixin.Common;
using Weixin.IWeixin;
using Weixin.Weixin;

namespace Weixin.Ashx
{
    /// <summary>
    ///     WeiXin 的摘要说明
    /// </summary>
    public class WeiXin : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                string postString;
                using (var stream = HttpContext.Current.Request.InputStream)
                {
                    var postBytes = new byte[stream.Length];
                    stream.Read(postBytes, 0, (int)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);
                }

                if (!string.IsNullOrEmpty(postString))
                {
                    TextLogHelper.WriteLog(postString);
                    Execute(postString);
                }
                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                HttpContext.Current.Response.Write("66");
            }
            else
            {

                TextLogHelper.WriteLog("----开始----");
                Auth();
                TextLogHelper.WriteLog("----结束----");

            }
        }
        /// <summary>
        /// 处理各种请求信息并应答（通过POST的请求）
        /// </summary>
        /// <param name="postStr">POST方式提交的数据</param>
        private void Execute(string postStr)
        {
            TextLogHelper.WriteLog("1");

            IWeixinAction weixinAction = new WeixinAction();
            string responseContent = weixinAction.Handle(postStr);
            TextLogHelper.WriteLog("5");

            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.Write(responseContent);
        }
        public bool IsReusable
        {
            get { return false; }
        }

        /// <summary>
        ///    验证
        /// </summary>
        private void Auth()
        {
            var token = ConfigurationManager.AppSettings["Token"]; //从配置文件获取Token
            if (string.IsNullOrEmpty(token))
            {
                TextLogHelper.WriteLog("Token 配置项没有配置！");
            }
            var echoString = HttpContext.Current.Request.QueryString["echoStr"];
            var signature = HttpContext.Current.Request.QueryString["signature"];
            var timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            var nonce = HttpContext.Current.Request.QueryString["nonce"];
            TextLogHelper.WriteLog("echoString:" + echoString);
            TextLogHelper.WriteLog("signature:" + signature);
            TextLogHelper.WriteLog("timestamp:" + timestamp);
            TextLogHelper.WriteLog("nonce:" + nonce);
            TextLogHelper.WriteLog("token:" + token);
            if (!CheckSignature(token, signature, timestamp, nonce)) return;
            if (string.IsNullOrEmpty(echoString)) return;
            HttpContext.Current.Response.Write(echoString);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        ///     验证微信签名
        /// </summary>
        public bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            string[] arrTmp = { token, timestamp, nonce };
            Array.Sort(arrTmp);
            var tmpStr = string.Join("", arrTmp);
            TextLogHelper.WriteLog("tmpStr:" + tmpStr);
            var tmpStrs = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, FormsAuthPasswordFormat.SHA1.ToString());
            TextLogHelper.WriteLog("tmpStrs:" + tmpStrs);
            TextLogHelper.WriteLog("signature:" + signature);
            return tmpStrs != null && tmpStrs.ToLower() == signature;
        }
    }
}
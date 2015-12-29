using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Weixin.Common;
using Weixin.IWeixin;
using Weixin.Model;
using Weixin.Model.Common;
using Weixin.Model.Enum;
using Weixin.Model.Request;
using Weixin.Model.Response;

namespace Weixin.Weixin
{

    public class WeixinAction : IWeixinAction
    {


        public string Handle(string postStr)
        {
            try
            {
                var baseInfo = XmlSerializerHelper.XmlToObject<BaseMessage>(postStr);
                ResponseText response = new ResponseText(baseInfo);
                if (baseInfo != null)
                {
                    var requestType = baseInfo.MsgType;
                    if (requestType == RequestMsgType.Text.ToString().ToLower())
                    {
                        RequestText info = XmlSerializerHelper.XmlToObject<RequestText>(postStr);
                        response.Content = info.Content;
                    }
                    else if (requestType == RequestMsgType.Image.ToString().ToLower())
                    {
                        RequestImage info = XmlSerializerHelper.XmlToObject<RequestImage>(postStr);
                        response.Content = info.PicUrl + ":" + info.MediaId + ";" + info.MsgId;
                    }
                    else if (requestType==RequestMsgType.Location.ToString().ToLower())
                    {
                        RequestLocation info = XmlSerializerHelper.XmlToObject<RequestLocation>(postStr);
                        response.Content = info.Label;
                    }
                    else
                    {
                        TextLogHelper.WriteLog("没有该处理事件,MsgType:" + baseInfo.MsgType);
                        response.Content = baseInfo.MsgType;
                    }
                    response.Content = "您好,你发送的信息为:" + response.Content;
                    return XmlSerializerHelper.ObjectToXml(response);
                }
                else
                {
                    TextLogHelper.WriteLog("post信息为空!");
                    return "success";
                }
            }
            catch (Exception e)
            {
                TextLogHelper.WriteLog("处理接收信息时报错:" + e.Message);
                return "success";
            }
        }



        public string HandleText(RequestText info)
        {
            if (info.Content.Trim().Contains("66"))
            {
                return "六六测试玩";
            }
            else
            {
                return "66";
            }
        }

        public string HandleImage(RequestImage info)
        {
            throw new System.NotImplementedException();
        }


    }
}
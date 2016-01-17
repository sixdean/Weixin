using System;
using Weixin.BLL.Common;
using Weixin.IWeixin;
using Weixin.Model.Common;
using Weixin.Model.Enum;
using Weixin.Model.Request;
using Weixin.Model.Response;

namespace Weixin.BLL.Weixin
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
                    if (requestType == RequestMsgType.text.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestText>(postStr);
                        response.Content = info.Content;
                    }
                    else if (requestType == RequestMsgType.image.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestImage>(postStr);
                        response.Content = info.PicUrl + ":" + info.MediaId + ";" + info.MsgId;
                    }
                    else if (requestType == RequestMsgType.voice.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestVoice>(postStr);

                    }
                    else if (requestType == RequestMsgType.video.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestVideo>(postStr);

                    }
                    else if (requestType == RequestMsgType.shortvideo.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestShortVideo>(postStr);

                    }
                    else if (requestType == RequestMsgType.location.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestLocation>(postStr);

                    }
                    else if (requestType == RequestMsgType.link.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestLink>(postStr);

                    }
                    else if (requestType == RequestMsgType.Event.ToString().ToLower())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestEvent>(postStr);

                    }
                    else if (requestType == RequestEventType.subscribe.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestEventSubscribe>(postStr);

                    }
                    else if (requestType == RequestEventType.unsubscribe.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestEventUnSubscribe>(postStr);

                    }
                    else if (requestType == RequestEventType.SCAN.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestEventScan>(postStr);

                    }
                    else if (requestType == RequestEventType.LOCATION.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestEventLocation>(postStr);

                    }
                    else if (requestType == RequestEventType.CLICK.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestEventClick>(postStr);

                    }
                    else if (requestType == RequestEventType.VIEW.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestEventView>(postStr);

                    }
                    else if (requestType == RequestEventType.scancode_push.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestEventScancode_push>(postStr);

                    }
                    else if (requestType == RequestEventType.scancode_waitmsg.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestEventScancode_waitmsg>(postStr);

                    }
                    else if (requestType == RequestEventType.pic_sysphoto.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestEventPic_sysphoto>(postStr);

                    }
                    else if (requestType == RequestEventType.pic_photo_or_album.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestEventPic_photo_or_album>(postStr);

                    }
                    else if (requestType == RequestEventType.pic_weixin.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestEventPic_weixin>(postStr);

                    }
                    else if (requestType == RequestEventType.location_select.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestEventLocation_select>(postStr);

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
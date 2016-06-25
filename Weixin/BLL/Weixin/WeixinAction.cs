using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
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
        /// <summary>
        ///     处理接收信息
        /// </summary>
        /// <param name="postStr"></param>
        /// <returns></returns>
        public string Handle(string postStr)
        {
            try
            {
                var baseInfo = XmlSerializerHelper.XmlToObject<BaseMessage>(postStr);
                var response = new ResponseText(baseInfo);
                var weixin = new Weixin();
                if (baseInfo != null)
                {
                    //消息类型
                    var requestType = baseInfo.MsgType;
                    #region 接收普通消息
                    //文本消息
                    if (requestType == RequestMsgType.text.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestText>(postStr);
                        if (info.Content.StartsWith("天气#") && info.Content.Split('#').Count() > 1)
                        {
                            var cityName = info.Content.Split('#')[1].Trim();
                            response.Content = weixin.ShowWeather(cityName);
                        }
                        else
                        {
                            response.Content = JsonConvert.SerializeObject(info);
                        }
                    } //图片消息
                    else if (requestType == RequestMsgType.image.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestImage>(postStr);
                        response.Content = JsonConvert.SerializeObject(info);

                    } //语音消息
                    else if (requestType == RequestMsgType.voice.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestVoice>(postStr);
                        response.Content = JsonConvert.SerializeObject(info);
                    } //视频消息
                    else if (requestType == RequestMsgType.video.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestVideo>(postStr);
                        response.Content = JsonConvert.SerializeObject(info);
                    } //小视频消息
                    else if (requestType == RequestMsgType.shortvideo.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestShortVideo>(postStr);
                        response.Content = JsonConvert.SerializeObject(info);
                    } //地理位置消息
                    else if (requestType == RequestMsgType.location.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestLocation>(postStr);
                        response.Content = JsonConvert.SerializeObject(info);
                    } //链接消息
                    else if (requestType == RequestMsgType.link.ToString())
                    {
                        var info = XmlSerializerHelper.XmlToObject<RequestLink>(postStr);
                        response.Content = JsonConvert.SerializeObject(info);
                    }
                    #endregion
                    #region 接收事件消息
                    else if (requestType == RequestMsgType.Event.ToString().ToLower())
                    {
                        var requestEventInfo = XmlSerializerHelper.XmlToObject<RequestEvent>(postStr);
                        var eventTyep = requestEventInfo.Event;
                        #region 接收事件推送
                        if (eventTyep == RequestEventType.subscribe.ToString())
                        {
                            //用户未关注时，进行关注后的事件推送
                            var info = XmlSerializerHelper.XmlToObject<RequestEventScanUnSubscribe>(postStr);
                            if (string.IsNullOrEmpty(info.Ticket) && string.IsNullOrEmpty(info.EventKey))
                            {
                                //订阅事件
                                var info1 = XmlSerializerHelper.XmlToObject<RequestEventSubscribe>(postStr);
                                response.Content = "多谢关注六六测试账号~~";

                            }
                            else
                            {

                            }
                        } //取消订阅事件
                        else if (eventTyep == RequestEventType.unsubscribe.ToString())
                        {
                            var info = XmlSerializerHelper.XmlToObject<RequestEventUnSubscribe>(postStr);

                        } //扫描带参数二维码事件，用户已关注时的事件推送
                        else if (eventTyep == RequestEventType.SCAN.ToString())
                        {
                            var info = XmlSerializerHelper.XmlToObject<RequestEventScanSubscribe>(postStr);
                        } //上报地理位置事件
                        else if (eventTyep == RequestEventType.LOCATION.ToString())
                        {
                            var info = XmlSerializerHelper.XmlToObject<RequestEventLocation>(postStr);
                        }
                        #endregion

                        #region 自定义菜单事件推送

                        //点击菜单拉取消息时的事件推送
                        else if (eventTyep == RequestEventType.CLICK.ToString())
                        {
                            var info = XmlSerializerHelper.XmlToObject<RequestEventClick>(postStr);
                            if (info.EventKey == "v_weather_select")
                            {
                                response.Content = "查询天气请用:天气#城市名称(国内城市支持中英文，国际城市支持英文)";
                            }
                            else if (info.EventKey == "test")
                            {
                                response.Content = "这只是个测试~~";
                            }
                            else
                            {
                                response.Content = "还在coding...";
                            }
                        } //点击菜单跳转链接时的事件推送
                        else if (eventTyep == RequestEventType.VIEW.ToString())
                        {
                            var info = XmlSerializerHelper.XmlToObject<RequestEventView>(postStr);
                        } //扫码推事件的事件推送
                        else if (eventTyep == RequestEventType.scancode_push.ToString())
                        {
                            var info = XmlSerializerHelper.XmlToObject<RequestEventScancode_push>(postStr);
                        } //扫码推事件且弹出“消息接收中”提示框的事件推送
                        else if (eventTyep == RequestEventType.scancode_waitmsg.ToString())
                        {
                            var info = XmlSerializerHelper.XmlToObject<RequestEventScancode_waitmsg>(postStr);
                        } //弹出系统拍照发图的事件推送
                        else if (eventTyep == RequestEventType.pic_sysphoto.ToString())
                        {
                            var info = XmlSerializerHelper.XmlToObject<RequestEventPic_sysphoto>(postStr);
                        } //弹出拍照或者相册发图的事件推送
                        else if (eventTyep == RequestEventType.pic_photo_or_album.ToString())
                        {
                            var info = XmlSerializerHelper.XmlToObject<RequestEventPic_photo_or_album>(postStr);
                        } //弹出微信相册发图器的事件推送
                        else if (eventTyep == RequestEventType.pic_weixin.ToString())
                        {
                            var info = XmlSerializerHelper.XmlToObject<RequestEventPic_weixin>(postStr);
                        } //弹出地理位置选择器的事件推送
                        else if (eventTyep == RequestEventType.location_select.ToString())
                        {
                            var info = XmlSerializerHelper.XmlToObject<RequestEventLocation_select>(postStr);
                        }
                        #endregion
                    }
                    #endregion
                    else
                    {
                        TextLogHelper.WriteLog("没有该处理事件,MsgType:" + baseInfo.MsgType);
                        response.Content = baseInfo.MsgType;
                    }
                    //response.Content = "您好,你发送的信息为:" + response.Content;
                    return XmlSerializerHelper.ObjectToXml(response);
                }
                TextLogHelper.WriteLog("post信息为空!");
                return "success";
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
            return "66";
        }

        public string HandleImage(RequestImage info)
        {
            throw new NotImplementedException();
        }
    }
}
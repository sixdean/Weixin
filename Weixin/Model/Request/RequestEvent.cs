using Weixin.Model.Common;
using Weixin.Model.Enum;
using Weixin.Weixin;

namespace Weixin.Model.Request
{
    /// <summary>
    /// 接受事件推送
    /// </summary>
    public class RequestEvent : BaseMessage
    {
        public RequestEvent()
        {
            this.MsgType = RequestMsgType.Event.ToString().ToLower();
        }
        public RequestEvent(BaseMessage info)
            : this()
        {
            this.ToUserName = info.ToUserName;
            this.FromUserName = info.FromUserName;
        }

        /// <summary>
        /// 事件类型
        /// </summary>
        public string Event { get; set; }
    }

    /// <summary>
    /// 关注事件
    /// </summary>
    public class RequestEventSubscribe : RequestEvent
    {
        public RequestEventSubscribe()
        {
            this.Event = RequestEventType.subscribe.ToString();
        }
    }

    /// <summary>
    /// 取消关注事件
    /// </summary>
    public class RequestEventUnSubscribe : RequestEvent
    {
        public RequestEventUnSubscribe()
        {
            this.Event = RequestEventType.unsubscribe.ToString();
        }
    }

    /// <summary>
    /// 扫描带参数二维码事件,用户未关注时
    /// </summary>
    public class RequestEventScanSubscribe : RequestEvent
    {
        public RequestEventScanSubscribe()
        {
            this.Event = RequestEventType.subscribe.ToString();
        }

        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值 
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片 
        /// </summary>
        public string Ticket { get; set; }
    }

    /// <summary>
    /// 扫描带参数二维码事件,用户已关注时的事件推送
    /// </summary>
    public class RequestEventScan : RequestEvent
    {
        public RequestEventScan()
        {
            this.Event = RequestEventType.SCAN.ToString();
        }

        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值 
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片 
        /// </summary>
        public string Ticket { get; set; }
    }

    /// <summary>
    /// 上报地理位置事件
    /// </summary>
    public class RequestEventLocation : RequestEvent
    {
        public RequestEventLocation()
        {
            this.Event = RequestEventType.LOCATION.ToString();
        }

        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 地理位置精度
        /// </summary>
        public string Precision { get; set; }
    }

    /// <summary>
    /// 自定义菜单事件,点击菜单拉取消息时的事件推送
    /// </summary>
    public class RequestEventClick : RequestEvent
    {
        public RequestEventClick()
        {
            this.Event = RequestEventType.CLICK.ToString();
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }
    }

    /// <summary>
    /// 自定义菜单事件,点击菜单跳转链接时的事件推送
    /// </summary>
    public class RequestEventView : RequestEvent
    {
        public RequestEventView()
        {
            this.Event = RequestEventType.VIEW.ToString();
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }
    }
}
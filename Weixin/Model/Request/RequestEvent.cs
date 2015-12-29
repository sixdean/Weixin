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

    /// <summary>
    ///  扫码推事件的事件推送
    /// </summary>
    public class RequestEventScancode_push : RequestEvent
    {
        public RequestEventScancode_push()
        {
            this.Event = RequestEventType.scancode_push.ToString();
        }

        /// <summary>
        /// 事件KEY值，由开发者在创建菜单时设定
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 扫描信息
        /// </summary>
        public string ScanCodeInfo { get; set; }

        /// <summary>
        /// 扫描类型，一般是qrcode
        /// </summary>
        public string ScanType { get; set; }

        /// <summary>
        /// 扫描结果，即二维码对应的字符串信息 
        /// </summary>
        public string ScanResult { get; set; }
    }

    /// <summary>
    ///  扫码推事件且弹出“消息接收中”提示框的事件推送
    /// </summary>
    public class RequestEventScancode_waitmsg : RequestEvent
    {
        public RequestEventScancode_waitmsg()
        {
            this.Event = RequestEventType.scancode_waitmsg.ToString();
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 扫描信息
        /// </summary>
        public string ScanCodeInfo { get; set; }

        /// <summary>
        /// 扫描类型，一般是qrcode
        /// </summary>
        public string ScanType { get; set; }

        /// <summary>
        /// 扫描结果，即二维码对应的字符串信息 
        /// </summary>
        public string ScanResult { get; set; }
    }

    /// <summary>
    ///  弹出系统拍照发图的事件推送
    /// </summary>
    public class RequestEventPic_sysphoto : RequestEvent
    {
        public RequestEventPic_sysphoto()
        {
            this.Event = RequestEventType.pic_sysphoto.ToString();
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 发送的图片信息
        /// </summary>
        public string SendPicsInfo { get; set; }

        /// <summary>
        /// 发送的图片数量
        /// </summary>
        public string Count { get; set; }

        /// <summary>
        /// 图片列表
        /// </summary>
        public string PicList { get; set; }

        /// <summary>
        /// 图片的MD5值，开发者若需要，可用于验证接收到图片 
        /// </summary>
        public string PicMd5Sum { get; set; }
    }

    /// <summary>
    ///  弹出拍照或者相册发图的事件推送
    /// </summary>
    public class RequestEventPic_photo_or_album : RequestEvent
    {
        public RequestEventPic_photo_or_album()
        {
            this.Event = RequestEventType.pic_photo_or_album.ToString();
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }


        /// <summary>
        /// 发送的图片信息
        /// </summary>
        public string SendPicsInfo { get; set; }

        /// <summary>
        /// 发送的图片数量
        /// </summary>
        public string Count { get; set; }

        /// <summary>
        /// 图片列表
        /// </summary>
        public string PicList { get; set; }

        /// <summary>
        /// 图片的MD5值，开发者若需要，可用于验证接收到图片 
        /// </summary>
        public string PicMd5Sum { get; set; }
    }

    /// <summary>
    ///  弹出微信相册发图器的事件推送
    /// </summary>
    public class RequestEventPic_weixin : RequestEvent
    {
        public RequestEventPic_weixin()
        {
            this.Event = RequestEventType.pic_weixin.ToString();
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }


        /// <summary>
        /// 发送的图片信息
        /// </summary>
        public string SendPicsInfo { get; set; }

        /// <summary>
        /// 发送的图片数量
        /// </summary>
        public string Count { get; set; }

        /// <summary>
        /// 图片列表
        /// </summary>
        public string PicList { get; set; }

        /// <summary>
        /// 图片的MD5值，开发者若需要，可用于验证接收到图片 
        /// </summary>
        public string PicMd5Sum { get; set; }
    }

    /// <summary>
    ///  弹出地理位置选择器的事件推送
    /// </summary>
    public class RequestEventLocation_select : RequestEvent
    {
        public RequestEventLocation_select()
        {
            this.Event = RequestEventType.location_select.ToString();
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        ///  发送的位置信息
        /// </summary>
        public string SendLocationInfo { get; set; }

        /// <summary>
        ///  X坐标信息
        /// </summary>
        public string Location_X { get; set; }

        /// <summary>
        ///  Y坐标信息
        /// </summary>
        public string Location_Y { get; set; }

        /// <summary>
        ///  精度，可理解为精度或者比例尺、越精细的话 scale越高
        /// </summary>
        public string Scale { get; set; }

        /// <summary>
        ///  地理位置的字符串信息
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        ///  朋友圈POI的名字，可能为空 
        /// </summary>
        public string Poiname { get; set; }
    }
}







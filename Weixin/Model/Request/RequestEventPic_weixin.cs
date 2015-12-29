using Weixin.Model.Enum;

namespace Weixin.Model.Request
{

    /// <summary>
    ///  弹出微信相册发图器的事件推送
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
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
}
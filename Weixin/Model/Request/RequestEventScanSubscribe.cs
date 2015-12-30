using Weixin.Model.Enum;

namespace Weixin.Model.Request
{
    /// <summary>
    /// 扫描带参数二维码事件,用户未关注时
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
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
}
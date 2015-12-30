using Weixin.Model.Enum;

namespace Weixin.Model.Request
{
    /// <summary>
    /// 扫描带参数二维码事件,用户已关注时的事件推送
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
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

}
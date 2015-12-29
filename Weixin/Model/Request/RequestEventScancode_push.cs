using Weixin.Model.Enum;

namespace Weixin.Model.Request
{
    /// <summary>
    ///  扫码推事件的事件推送
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
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
}
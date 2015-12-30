using Weixin.Model.Enum;

namespace Weixin.Model.Request
{
    /// <summary>
    ///  扫码推事件且弹出“消息接收中”提示框的事件推送
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
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
}
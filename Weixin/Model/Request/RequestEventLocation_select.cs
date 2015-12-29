using Weixin.Model.Enum;

namespace Weixin.Model.Request
{

    /// <summary>
    ///  弹出地理位置选择器的事件推送
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
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
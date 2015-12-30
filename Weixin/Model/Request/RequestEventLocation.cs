using Weixin.Model.Enum;

namespace Weixin.Model.Request
{

    /// <summary>
    /// 上报地理位置事件
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
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
}
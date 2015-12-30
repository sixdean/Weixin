using Weixin.Model.Enum;

namespace Weixin.Model.Request
{
    /// <summary>
    /// 取消关注事件
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
    public class RequestEventUnSubscribe : RequestEvent
    {
        public RequestEventUnSubscribe()
        {
            this.Event = RequestEventType.unsubscribe.ToString();
        }
    }

}
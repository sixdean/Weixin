using Weixin.Model.Enum;

namespace Weixin.Model.Request
{ 
    /// <summary>
    /// 关注事件
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
    public class RequestEventSubscribe : RequestEvent
    {
        public RequestEventSubscribe()
        {
            this.Event = RequestEventType.subscribe.ToString();
        }
    }
}
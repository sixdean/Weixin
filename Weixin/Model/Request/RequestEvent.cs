using Weixin.Model.Common;
using Weixin.Model.Enum;


namespace Weixin.Model.Request
{
    /// <summary>
    /// 接受事件推送
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]   
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

   
}







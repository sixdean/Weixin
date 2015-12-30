using Weixin.Model.Enum;

namespace Weixin.Model.Request
{


    /// <summary>
    /// 自定义菜单事件,点击菜单拉取消息时的事件推送
    /// </summary>
     [System.Xml.Serialization.XmlRoot(ElementName = "xml")]   
    public class RequestEventClick : RequestEvent
    {
        public RequestEventClick()
        {
            this.Event = RequestEventType.CLICK.ToString();
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }
    }

}
using Weixin.Model.Common;
using Weixin.Model.Enum;
using Weixin.Weixin;

namespace Weixin.Model.Response
{
    /// <summary>
    /// 回复文本消息
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
    public class ResponseText : BaseMessage
    {
        public ResponseText()
        {
            this.MsgType = ResponseMsgType.text.ToString().ToLower();
        }

        public ResponseText(BaseMessage info)
            : this()
        {
            this.FromUserName = info.ToUserName;
            this.ToUserName = info.FromUserName;
        }

        /// <summary>
        /// 内容
        /// </summary>        
        public string Content { get; set; }
    }
}
using System;
using Weixin.Model.Common;
using Weixin.Model.Enum;
using Weixin.Weixin;

namespace Weixin.Model.Request
{

    /// <summary>
    /// 接收链接消息
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
    public class RequestLink : BaseMessage
    {
        public RequestLink()
        {
            this.MsgType = RequestMsgType.link.ToString().ToLower();
        }

        public RequestLink(BaseMessage info)
            : this()
        {
            this.ToUserName = info.ToUserName;
            this.FromUserName = info.FromUserName;
        }

        /// <summary>
        ///  消息标题 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 消息id，64位整型 
        /// </summary>
        public Int64 MsgId { get; set; }

    }
}
using System;
using Weixin.Model.Common;
using Weixin.Model.Enum;


namespace Weixin.Model.Request
{
    /// <summary>
    /// 接收图片消息
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
    public class RequestImage : BaseMessage
    {
        public RequestImage()
        {
            this.MsgType = RequestMsgType.image.ToString().ToLower();
        }

        public RequestImage(BaseMessage info)
            : this()
        {
            this.ToUserName = info.ToUserName;
            this.FromUserName = info.FromUserName;
        }
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 消息id，64位整型 
        /// </summary>
        public Int64 MsgId { get; set; }
    }
}
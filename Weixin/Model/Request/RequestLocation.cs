using System;
using Weixin.Weixin;

namespace Weixin.Model.Request
{

    /// <summary>
    /// 接收地理位置消息
    /// </summary>
    public class RequestLocation : BaseMessage
    {
        public RequestLocation()
        {
            this.MsgType = RequestMsgType.Location.ToString().ToLower();
        }
        public RequestLocation(BaseMessage info)
            : this()
        {
            this.ToUserName = info.ToUserName;
            this.FromUserName = info.FromUserName;
        }

        /// <summary>
        /// 地理位置维度
        /// </summary>
        public string Location_X { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Location_Y { get; set; }
        /// <summary>
        ///  地图缩放大小
        /// </summary>
        public string Scale { get; set; }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public Int64 MsgId { get; set; }
    }
}
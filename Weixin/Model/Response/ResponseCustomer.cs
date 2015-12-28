using Weixin.Weixin;

namespace Weixin.Model.Response
{

    /// <summary>
    /// 回复客服消息
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
    public class ResponseCustomer : BaseMessage
    {
        public ResponseCustomer()
        {
            this.MsgType = ResponseMsgType.Transfer_Customer_Service.ToString().ToLower();
        }

        public ResponseCustomer(BaseMessage info)
            : this()
        {
            this.FromUserName = info.ToUserName;
            this.ToUserName = info.FromUserName;
        }
    }
}
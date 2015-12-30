using Weixin.Model.Common;
using Weixin.Model.Enum;
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
            this.MsgType = ResponseMsgType.transfer_customer_service.ToString().ToLower();
        }

        public ResponseCustomer(BaseMessage info)
            : this()
        {
            this.FromUserName = info.ToUserName;
            this.ToUserName = info.FromUserName;
        }
    }
}
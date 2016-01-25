using System.Xml.Serialization;
using Weixin.Model.Common;
using Weixin.Model.Enum;

namespace Weixin.Model.Response
{
    /// <summary>
    ///     回复客服消息
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class ResponseCustomer : BaseMessage
    {
        public ResponseCustomer()
        {
            MsgType = ResponseMsgType.transfer_customer_service.ToString().ToLower();
        }

        public ResponseCustomer(BaseMessage info)
            : this()
        {
            FromUserName = info.ToUserName;
            ToUserName = info.FromUserName;
        }
    }
}
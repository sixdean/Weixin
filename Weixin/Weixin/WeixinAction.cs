using System;
using System.IO;
using Weixin.Common;
using Weixin.IWeixin;
using Weixin.Model;
using Weixin.Model.Common;
using Weixin.Model.Request;
using Weixin.Model.Response;

namespace Weixin.Weixin
{

    public class WeixinAction : IWeixinAction
    {


        public string Handle(string postStr)
        {
            try
            {
                var baseinfo = XmlSerializerHelper.XmlToObject<BaseMessage>(postStr);
                if (baseinfo != null)
                {
                    var result = "";
                    switch (baseinfo.MsgType)
                    {
                        case "text":
                            {
                                RequestText info = XmlSerializerHelper.XmlToObject<RequestText>(postStr);
                                TextLogHelper.WriteLog("3");

                                if (info != null)
                                {
                                    result = HandleText(info);
                                }
                            }
                            break;
                        case "image":
                            {
                                RequestImage info = XmlSerializerHelper.XmlToObject<RequestImage>(postStr);
                                if (info != null)
                                {
                                    result = HandleImage(info);
                                }
                            }
                            break;
                        default:
                            TextLogHelper.WriteLog("没有该处理事件!");
                            return "";
                    }
                    TextLogHelper.WriteLog("4");



                    return result;
                }
                else
                {
                    TextLogHelper.WriteLog("post信息为空!");
                    return "success";

                }
            }
            catch (Exception e)
            {
                TextLogHelper.WriteLog("5" + e.Message);

                return "success";

            }
        }

        public string HandleText(RequestText info)
        {
            if (info.Content.Trim().Contains("66"))
            {
                return "六六测试玩";
            }
            else
            {
                return "66";
            }
        }

        public string HandleImage(RequestImage info)
        {
            throw new System.NotImplementedException();
        }


    }
}
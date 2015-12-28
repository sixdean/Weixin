using Weixin.Model;

namespace Weixin.Common
{
    public static class Helper
    {
          

        /// <summary>
        /// 通用的操作结果
        /// </summary>
        /// <param name="url">网页地址</param>
        /// <param name="postData">提交的数据内容</param>
        /// <returns></returns>
        public static CommonResult GetExecuteResult(string url, string postData = null)
        {
            CommonResult success = new CommonResult();
            try
            {
                ErrorJsonResult result;
                if (postData != null)
                {
                    result = JsonHelper<ErrorJsonResult>.ConvertJson(url, postData);
                }
                else
                {
                    result = JsonHelper<ErrorJsonResult>.ConvertJson(url);
                }

                if (result != null)
                {
                    //success.Success = (result.errcode == ReturnCode.请求成功);
                    //success.ErrorMessage = result.errmsg;
                }
            }
            catch
            {
            }
            //catch (WeixinException ex)
            //{
            //    success.ErrorMessage = ex.Message;
            //}

            return success;
        }  
    }
    }
 
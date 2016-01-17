namespace Weixin.Model.Common
{
    /// <summary>
    /// 微信返回Json结果的错误数据
    /// </summary>
    public class ErrorJsonResult
    {
        

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrMsg { get; set; }

        /// <summary>
        /// 返回代码
        /// </summary>
        public int ErrCode { get; set; }

    }
}
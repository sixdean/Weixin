namespace Weixin.Model
{
    /// <summary>
    /// 微信返回Json结果的错误数据
    /// </summary>
    public class ErrorJsonResult
    {
        /// <summary>
        /// 返回代码
        /// </summary>
        //public ReturnCode errcode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrMsg { get; set; }

        public string ErrCode { get; set; }

    }
}
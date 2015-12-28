namespace Weixin.Weixin
{
    /// <summary>
    /// 分组信息
    /// </summary>
    public class GroupJson 
    {
        /// <summary>
        /// 分组id，由微信分配
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 分组名字，UTF8编码
        /// </summary>
        public string name { get; set; }
    }
}
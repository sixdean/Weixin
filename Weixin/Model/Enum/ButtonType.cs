namespace Weixin.Model.Enum
{
    /// <summary>
    /// 菜单按钮类型
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// 点击
        /// </summary>
        click,

        /// <summary>
        /// Url
        /// </summary>
        view,

        /// <summary>
        /// 扫码推事件的事件推送
        /// </summary>
        scancode_push,

        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框的事件推送
        /// </summary>
        scancode_waitmsg,

        /// <summary>
        /// 弹出系统拍照发图的事件推送
        /// </summary>
        pic_sysphoto,

        /// <summary>
        /// 弹出拍照或者相册发图的事件推送
        /// </summary>
        pic_photo_or_album,

        /// <summary>
        /// 弹出微信相册发图器的事件推送
        /// </summary>
        pic_weixin,

        /// <summary>
        /// 弹出地理位置选择器的事件推送
        /// </summary>
        location_select,

        /// <summary>
        /// 下发消息（除文本消息）
        /// </summary>
        media_id,

        /// <summary>
        /// 跳转图文消息URL
        /// </summary>
        view_limited
    }
}
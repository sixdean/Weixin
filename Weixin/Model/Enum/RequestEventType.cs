namespace Weixin.Model.Enum
{
    public enum RequestEventType
    {
        /// <summary>
        /// 订阅
        /// </summary>
        subscribe,

        /// <summary>
        /// 取消订阅
        /// </summary>
        unsubscribe,

        /// <summary>
        /// 用户已关注时的事件推送
        /// </summary>
        SCAN,

        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        LOCATION,

        /// <summary>
        /// 点击菜单拉取消息时的事件推送
        /// </summary>
        CLICK,

        /// <summary>
        /// 点击菜单跳转链接时的事件推送
        /// </summary>
        VIEW,

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
        location_select
    }
}
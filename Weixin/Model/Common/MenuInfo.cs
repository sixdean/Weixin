using System.Collections.Generic;
using Newtonsoft.Json;
using Weixin.Model.Enum;

namespace Weixin.Model.Common
{

    /// <summary>
    /// 菜单基本信息
    /// </summary>
    public class MenuInfo
    {
        public MenuInfo()
        {

        } 
        /// <summary>
        /// 参数化构造函数
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <param name="buttonType">菜单按钮类型</param>
        /// <param name="value">按钮的键值（Click)，或者连接URL(View)</param>
        public MenuInfo(string name, ButtonType buttonType, string value, IEnumerable<MenuInfo> sub_button)
        {
            this.name = name;
            this.type = buttonType.ToString();
            if (buttonType == ButtonType.view)
            {
                this.url = value;
            }
            else
            {
                this.key = value;
            }
            if(sub_button!=null)
            this.sub_button.AddRange(sub_button);
        }
        /// <summary>
        /// 参数化构造函数,用于构造子菜单
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <param name="sub_button">子菜单集合</param>
        public MenuInfo(string name, IEnumerable<MenuInfo> sub_button)
        {
            this.name = name;
            this.sub_button = new List<MenuInfo>();
            this.sub_button.AddRange(sub_button);
        }

        /// <summary>
        /// 按钮描述，既按钮名字，不超过16个字节，子菜单不超过40个字节
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 菜单的响应动作类型 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string type { get; set; }

        /// <summary>
        /// click等点击类型必须 ,菜单KEY值，用于消息接口推送，不超过128字节 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string key { get; set; }

        /// <summary>
        /// view类型必须 ,网页链接，用户点击菜单可打开链接，不超过256字节 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string url { get; set; }

        /// <summary>
        /// 调用新增永久素材接口返回的合法media_id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string media_id { get; set; }

        /// <summary>
        /// 子按钮数组，按钮个数应为2~5个
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MenuInfo> sub_button { get; set; }
    }


    /// <summary>
    /// 菜单的Json字符串对象
    /// </summary>
    public class MenuJson
    {
        public List<MenuInfo> button { get; set; }

        public MenuJson()
        {
            button = new List<MenuInfo>();
        }
    }

    /// <summary>
    /// 菜单列表的Json对象
    /// </summary>
    public class MenuListJson
    {
        public MenuJson menu { get; set; }

        public MenuListJson()
        {
            menu = new MenuJson();
        }
    }


}
using Weixin.Common;
using Weixin.IWeixin;
using Weixin.Model;
using Weixin.Model.Common;

namespace Weixin.Weixin
{
    public class MenuApi:IMenuApi
    {
        /// <summary>
        /// 获取菜单数据
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <returns></returns>
        public MenuJson GetMenu(string accessToken)
        {
            MenuJson menu = null;

            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", accessToken);
            MenuListJson list = JsonHelper<MenuListJson>.ConvertJson(url);
            if (list != null)
            {
                menu = list.menu;
            }
            return menu;
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="menuJson">菜单对象</param>
        /// <returns></returns>
        public CommonResult CreateMenu(string accessToken, MenuJson menuJson)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", accessToken);
            string postData = menuJson.ToJson();

            return Helper.GetExecuteResult(url, postData);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <returns></returns>
        public CommonResult DeleteMenu(string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", accessToken);

            return Helper.GetExecuteResult(url);
        }
         
    }
}
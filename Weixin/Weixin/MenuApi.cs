using System;
using System.Collections.Generic;
using Weixin.Common;
using Weixin.DAL;
using Weixin.IWeixin;
using Weixin.Model;
using Weixin.Model.Common;

namespace Weixin.Weixin
{
    public class MenuApi : IMenuApi
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

        public List<Menu> GetListMenus(string accessToken)
        {
            List<Menu> listmMenus = new List<Menu>();
            var listmMenuInfos = GetMenu(accessToken).button;
            return AddMenus(listmMenus, listmMenuInfos, "");
        }

        public List<Menu> AddMenus(List<Menu> listmMenus, List<MenuInfo> listmMenuInfos, string parentid)
        {
            foreach (MenuInfo info in listmMenuInfos)
            {
                var menu = new Menu
                {
                    Id = Guid.NewGuid().ToString(),
                    MenuId = Guid.NewGuid().ToString(),
                    Type = info.type,
                    Media_id = info.media_id,
                    Key = info.key,
                    Name = info.name,
                    Url = info.url,
                    ParentId = parentid,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreateUser = "66",
                    UpdateUser = "66"
                };
                listmMenus.Add(menu);
                if (info.sub_button.Count > 0)
                {
                    AddMenus(listmMenus, info.sub_button, menu.MenuId);
                }
            }
            return listmMenus;
        }
    }
}
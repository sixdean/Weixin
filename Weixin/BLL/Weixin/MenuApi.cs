using System;
using System.Collections.Generic;
using System.Linq;
using Weixin.BLL.Common;
using Weixin.DAL;
using Weixin.IWeixin;
using Weixin.Model.Common;

namespace Weixin.BLL.Weixin
{
    public class MenuApi : IMenuApi
    {
        /// <summary>
        ///     获取菜单数据
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <returns></returns>
        public MenuJson GetMenu(string accessToken)
        {
            MenuJson menu = null;

            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", accessToken);
            var list = JsonHelper<MenuListJson>.ConvertJson(url);
            if (list != null)
            {
                menu = list.menu;
            }
            return menu;
        }

        /// <summary>
        ///     创建菜单
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="menuJson">菜单对象</param>
        /// <returns></returns>
        public ErrorJsonResult CreateMenu(string accessToken, MenuJson menuJson)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", accessToken);
            var postData = menuJson.ToJson();
            return Helper.GetExecuteResult(url, postData);
        }

        /// <summary>
        ///     删除菜单
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <returns></returns>
        public ErrorJsonResult DeleteMenu(string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", accessToken);

            return Helper.GetExecuteResult(url);
        }

        /// <summary>
        ///     根据accessToken获取微信服务器上菜单数据并转换为list<Menu>;
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public List<Menu> GetListMenus(string accessToken)
        {
            var listmMenus = new List<Menu>();
            var listmMenuInfos = GetMenu(accessToken).button;
            return AddSubMenus(listmMenus, listmMenuInfos, "");
        }

        /// <summary>
        ///     循环子菜单
        /// </summary>
        /// <param name="listmMenus"></param>
        /// <param name="listmMenuInfos"></param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public List<Menu> AddSubMenus(List<Menu> listmMenus, List<MenuInfo> listmMenuInfos, string parentid)
        {
            foreach (var info in listmMenuInfos)
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
                    AddSubMenus(listmMenus, info.sub_button, menu.MenuId);
                }
            }
            return listmMenus;
        }


        /// <summary>
        /// 根据本地数据库菜单数据转换为可以向微信服务器提交的json菜单数据
        /// </summary>
        /// <param name="listMenus"></param>
        /// <returns></returns>
        public MenuJson GetMenuListJson(IQueryable<Menu> listMenus)
        {
            var menuJson = new MenuJson { button = GetSubMenuButton(listMenus, "") };
            return menuJson;
        }

        /// <summary>
        /// 获取下级
        /// </summary>
        /// <param name="listMenus"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<MenuInfo> GetSubMenuButton(IQueryable<Menu> listMenus, string parentId)
        {
            var listMenuInfo = new List<MenuInfo>();
            foreach (var listMenu in listMenus.Where(o => o.ParentId == parentId).OrderBy(o => o.Sort))
            {
                var menuInfo = new MenuInfo { name = listMenu.Name };
                if (string.IsNullOrEmpty(listMenu.Type))
                {
                    menuInfo.sub_button = GetSubMenuButton(listMenus, listMenu.MenuId);
                }
                else
                {
                    menuInfo.name = listMenu.Name;
                    menuInfo.type = listMenu.Type;
                    var type = listMenu.Type;
                    switch (type)
                    {
                        case "view":
                            menuInfo.url = listMenu.Url;
                            break;
                        case "click":
                        case "scancode_waitmsg":
                        case "scancode_push":
                        case "pic_sysphoto":
                        case "pic_photo_or_album":
                        case "pic_weixin":
                        case "location_select":
                            menuInfo.key = listMenu.Key;
                            break;
                        case "media_id":
                        case "view_limited":
                            menuInfo.media_id = listMenu.Media_id;
                            break;
                    }
                }
                listMenuInfo.Add(menuInfo);
            }
            return listMenuInfo;
        }
    }
}
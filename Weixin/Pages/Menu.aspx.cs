using System;
using System.Configuration;
using System.Web.Services;
using Newtonsoft.Json;
using Weixin.BLL;
using Weixin.Weixin;

namespace Weixin.Pages
{
    public partial class Menu : BasePage
    {
        public static string accessToken = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            accessToken = AccessToken;
        }

        /// <summary>
        ///     将本地数据库更新到微信服务器
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string UpdateWeixinMenu()
        {

            var listMenu = FactoryBll<MenuBll>.Instance.GetAll();
            var menuApi = new MenuApi();
            var menuJson = menuApi.GetMenuListJson(listMenu);
            menuApi.CreateMenu(accessToken, menuJson);
            return JsonConvert.SerializeObject("ok");

        }

        /// <summary>
        ///     删除本地数据库
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string DeleteWeixinMenu()
        {
            return JsonConvert.SerializeObject("ok");
        }

        /// <summary>
        ///     保存到本地数据库
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string SaveWeixinMenu(string d)
        {
            var menu = JsonConvert.DeserializeObject<DAL.Menu>(d);
            var mbBll = new MenuBll();
            if (string.IsNullOrEmpty(menu.Id))
            {
                mbBll.AddMenu(menu);
            }
            else
            {
                mbBll.UpdateMenu(menu);
            }
            return JsonConvert.SerializeObject("ok");
        }

        /// <summary>
        ///     从微信服务器上获取菜单数据
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string GetWeixinMenu()
        {
            var menuApi = new MenuApi();
            var listMenu = menuApi.GetListMenus(accessToken);
            if (listMenu.Count > 0)
            {
                var menuBll = new MenuBll();
                menuBll.DeleteAll<DAL.Menu>();
                menuBll.Add(listMenu);
                return Common.Common.GetDatagridJsonString(listMenu);
            }
            return JsonConvert.SerializeObject("err");
        }
    }
}
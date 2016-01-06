using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Weixin.BLL;
using Weixin.DAL;
using Weixin.Common;
using Weixin.Weixin;

namespace Weixin.Pages
{
    public partial class Menu : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 将本地数据库更新到微信服务器
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string UpdateWeixinMenu()
        {
            return JsonConvert.SerializeObject("ok");
        }

        /// <summary>
        /// 删除本地数据库
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string DeleteWeixinMenu()
        {
            return JsonConvert.SerializeObject("ok");
        }

        /// <summary>
        /// 保存到本地数据库
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string SaveWeixinMenu(string d)
        {
            //DAL.Menu menu = JsonConvert.DeserializeObject<DAL.Menu>(d);
            //MenuBll mbBll = new MenuBll();
            //if (string.IsNullOrEmpty(menu.Id.ToString()))
            //{
            //    mbBll.AddMenu(menu);
            //}
            //else
            //{
            //    mbBll.UpdateMenu(menu);

            //}
            return JsonConvert.SerializeObject("ok");
        }

        /// <summary>
        /// 从微信服务器上获取菜单数据
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string GetWeixinMenu()
        {
            var accessToken = ConfigurationManager.AppSettings["access_token"];
            MenuApi menuApi = new MenuApi();
            var listMenu = menuApi.GetListMenus(accessToken);
            if (listMenu != null)
            {
                MenuBll menuBll = new MenuBll();
                menuBll.Delete<DAL.Menu>();
                menuBll.AddListMenu(listMenu);
                return Common.Common.GetDatagridJsonString(listMenu);
            }
            else
            {
                return JsonConvert.SerializeObject("err");
            }
        }
    }
}
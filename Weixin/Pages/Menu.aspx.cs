using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Weixin.BLL;
using Weixin.DAL;
namespace Weixin.Pages
{
    public partial class Menu : System.Web.UI.Page
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
            DAL.Menu menu = JsonConvert.DeserializeObject<DAL.Menu>(d);
            if (menu.Id == "")
            {
                FactoryBll<MenuBll>.Instance.Add(menu);
            }
            else
            {
                FactoryBll<MenuBll>.Instance.Update(menu);

            }
            return JsonConvert.SerializeObject("ok");
        }
    }
}
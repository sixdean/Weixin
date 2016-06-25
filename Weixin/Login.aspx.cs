using System;
using System.Net.Http;
using System.Web.Services;
using Newtonsoft.Json;
using Weixin.BLL;
using Weixin.BLL.Common;
using Weixin.DAL;
using Weixin.Model.Common;
using Weixin.Pages;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Text;
using System.Text.RegularExpressions;
namespace Weixin
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            Session["fdaf"] = "";
        }


        [WebMethod]
        public static string UserLogin(string d)
        {
            var reuslt = AjaxResultKeys.Error;
            var msg = "";
            var user = JsonConvert.DeserializeObject<SysUser>(d);
            var sysUserBll = new SysUserBll();
            if (sysUserBll.CheckUserId(user.UserId))
            {
                if (sysUserBll.CheckLogin(user.UserId, user.Password))
                {
                    reuslt = AjaxResultKeys.Succes;
                    UserProfiles userProfiles = SecurityManager.GetUserProfiles(user.UserId);
                    HttpContext.Current.Session[SessionKeys.CurUserCurUserProfiles] = userProfiles;
                    FormsAuthentication.SetAuthCookie(userProfiles.UserId, false);
                }
                else
                {
                    msg = "密码错误";
                }
            }
            else
            {
                msg = "账号错误";
            }
            return JsonConvert.SerializeObject(new AjaxResult(reuslt, msg));
        }
    }
}
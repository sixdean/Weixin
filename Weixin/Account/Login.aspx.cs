using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weixin.BLL;

namespace Weixin.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }


        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            SysUserBll sysUserBll = new SysUserBll();
            if (sysUserBll.CheckLogin(tbUserId.Text, tbPassword.Text))
            {
                Response.Redirect("../Default.aspx");
            }
        }
    }
}
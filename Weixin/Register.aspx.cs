using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weixin.Account
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }



        protected void FormView1_OnItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            if (e.Exception == null)
            {
                Response.Redirect("Login.aspx");

            }
            else
            {
            }
        }

        protected void ObjectDataSource1_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            var s = "dfasfjk";
        }
    }
}

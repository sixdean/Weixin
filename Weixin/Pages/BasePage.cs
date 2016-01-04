using System.Configuration;
using System.Web.UI;

namespace Weixin.Pages
{
    public class BasePage:Page
    {


        public string AccessToken
        {
            get { return ConfigurationManager.AppSettings["access_token"]; }
        }



    }
}
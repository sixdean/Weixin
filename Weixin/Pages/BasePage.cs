using System.Configuration;
using System.Runtime.Caching;
using System.Web.UI;

namespace Weixin.Pages
{
    public class BasePage : Page
    {
        private readonly ObjectCache cache = MemoryCache.Default;
        private readonly string appid = ConfigurationManager.AppSettings["appID"].ToString();
        private readonly string secret = ConfigurationManager.AppSettings["appsecret"];
        public string AccessToken
        {
            get
            {
                if (cache.Contains("AccessToken"))
                {
                    return (string)cache.Get("AccessToken");
                }
                return Common.Common.GetAccessToken(appid, secret);
            }
        }



    }
}
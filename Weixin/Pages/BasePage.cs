using System.Configuration;
using System.Runtime.Caching;
using System.Web.UI;
using Weixin.BLL.Common;
using Weixin.Model.Common;

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
                //return "kmnR5WBEmJlGNrDo7vlguvR83ih8sRV9EekC5zAKmTsJes-f7nNqH9-Xh-8lZzuVVeFoqcVzg-zrO3RevPRRQyM4_nwoLImPIA98aLJfaPgTJGhADAPFC";
                if (cache.Contains("AccessToken"))
                {
                    return (string)cache.Get("AccessToken");
                }
                return BLL.Common.Common.GetAccessToken(appid, secret);
            }
        }

        public UserProfiles CurUserProfiles
        {
            get
            {
                SessionManager sm = new SessionManager();
                return sm.CurUserProfiles;
            }
        }


    }
}
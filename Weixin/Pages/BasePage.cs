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
                return "fy8Nn2tE2KvAGehLHOBCd4laTD1MVvwXKW-qUMKXwagSNK1G881Gvzjb4sa7iGV5S3ht7bq_2YYSxuXrEND_uc7AHhKkQSArvDbplzbAneUFIKiACASVX";
                if (cache.Contains("AccessToken"))
                {
                    return (string)cache.Get("AccessToken");
                }
                return BLL.Common.Common.GetAccessToken(appid, secret);
            }
        }



    }
}
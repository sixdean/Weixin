using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Newtonsoft.Json;
using Weixin.BLL;
using Weixin.DAL;
using Weixin.Weixin;

namespace Weixin.Pages
{
    public partial class UserInfo : BasePage
    {
        public static string accessToken = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            accessToken = AccessToken;

        }

        [WebMethod]
        public static string GetWeixinGroupData()
        {
            UserApi userApi = new UserApi();
            var groupList = userApi.GetWeixinGroupData(accessToken);
            GroupInfoBll groupBll = new GroupInfoBll();
            groupBll.DeleteAll<GroupInfo>();
            groupBll.Add(groupList.groups);
            return JsonConvert.SerializeObject(groupList.groups);
        }


        [WebMethod]
        public static string GetWeixinUserInfoData()
        {
            UserApi userApi = new UserApi();
            var userList = userApi.GetUserList(accessToken);
            //UserInfoBll userInfoBll = new UserInfoBll();
            //userInfoBll.DeleteAll<GroupInfo>();
            //userInfoBll.Add(groupList.groups);
            return JsonConvert.SerializeObject(userList);
        }
    }
}
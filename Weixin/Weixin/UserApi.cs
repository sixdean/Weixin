using System.Collections.Generic;
using Weixin.Common;
using Weixin.IWeixin;
using Weixin.Model;

namespace Weixin.Weixin
{
    class UserApi : IUserApi
    {
        /// <summary>
        /// 获取关注用户列表
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="nextOpenId">第一个拉取的OPENID，不填默认从头开始拉取</param>
        /// <returns></returns>
        public List<string> GetUserList(string accessToken, string nextOpenId = null)
        {
            List<string> list = new List<string>();

            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}", accessToken);
            if (!string.IsNullOrEmpty(nextOpenId))
            {
                url += "&next_openid=" + nextOpenId;
            }

            UserListJsonResult result = JsonHelper<UserListJsonResult>.ConvertJson(url);
            if (result != null && result.data != null)
            {
                list.AddRange(result.data.openid);
            }

            return list;
        }
         

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        public UserJson GetUserDetail(string accessToken, string openId, Language lang = Language.zh_CN)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang={2}",
                   accessToken, openId, lang.ToString());

            UserJson result = JsonHelper<UserJson>.ConvertJson(url);
            return result;
        }


        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="name">分组名称</param>
        /// <returns></returns>
        public GroupJson CreateGroup(string accessToken, string name)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}", accessToken);
            var data = new
            {
                group = new
                {
                    name = name
                }
            };
            string postData = data.ToJson();

            GroupJson group = null;
            //CreateGroupResult result = JsonHelper<CreateGroupResult>.ConvertJson(url, postData);
            //if (result != null)
            //{
            //    group = result.group;
            //}
            return group;
        }

        /// <summary>
        /// 查询所有分组
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <returns></returns>
        //public List<GroupJson> GetGroupList(string accessToken)
        //{
        //    string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}", accessToken);

        //    List<GroupJson> list = new List<GroupJson>();
        //    GroupListJsonResult result = JsonHelper<GroupListJsonResult>.ConvertJson(url);
        //    if (result != null && result.groups != null)
        //    {
        //        list.AddRange(result.groups);
        //    }

        //    return list;
        //}

        /// <summary>
        /// 查询用户所在分组
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="openid">用户的OpenID</param>
        /// <returns></returns>
        public int GetUserGroupId(string accessToken, string openid)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/getid?access_token={0}", accessToken);
            var data = new
            {
                openid = openid
            };
            string postData = data.ToJson();

            int groupId = -1;
            //GroupIdJsonResult result = JsonHelper<GroupIdJsonResult>.ConvertJson(url, postData);
            //if (result != null)
            //{
            //    groupId = result.groupid;
            //}
            return groupId;
        }


        /// <summary>
        /// 修改分组名  
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="id">分组id，由微信分配</param>
        /// <param name="name">分组名字（30个字符以内）</param>
        /// <returns></returns>
        public CommonResult UpdateGroupName(string accessToken, int id, string name)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}", accessToken);
            var data = new
            {
                group = new
                {
                    id = id,
                    name = name
                }
            };
            string postData = data.ToJson();

            return Helper.GetExecuteResult(url, postData);
        }

        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="openid">用户的OpenID</param>
        /// <param name="to_groupid">分组id</param>
        /// <returns></returns>
        public CommonResult MoveUserToGroup(string accessToken, string openid, int to_groupid)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token={0}", accessToken);
            var data = new
            {
                openid = openid,
                to_groupid = to_groupid
            };
            string postData = data.ToJson();

            return Helper.GetExecuteResult(url, postData);
        }
    }
}
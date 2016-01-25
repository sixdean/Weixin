using System.Collections.Generic;
using System.Linq;
using Weixin.BLL.Common;
using Weixin.DAL;
using Weixin.IWeixin;
using Weixin.Model.Common;
using Weixin.Model.Enum;


namespace Weixin.BLL.Weixin
{
    class UserApi : IUserApi
    {

        #region 用户分组管理
        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="name">分组名称</param>
        /// <returns></returns>
        public GroupJson CreateGroup(string accessToken, string name)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}", accessToken);
            var data = new
            {
                group = new
                {
                    name = name
                }
            };
            var postData = data.ToJson();
            GroupJson group = null;
            var result = JsonHelper<CreateGroupJson>.ConvertJson(url, postData);
            if (result != null && result.group != null)
            {
                group = result.group;
            }
            return group;
        }

        /// <summary>
        /// 查询所有分组
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <returns></returns>
        public List<GroupInfo> GetGroupList(string accessToken)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}", accessToken);

            var list = new List<GroupInfo>();
            var result = JsonHelper<GroupListJson>.ConvertJson(url);
            if (result != null && result.groups != null)
            {
                list.AddRange(result.groups.Select(groupJson => new GroupInfo(groupJson)));
            }
            return list;
        }



        /// <summary>
        /// 查询用户所在分组
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="openid">用户的OpenID</param>
        /// <returns></returns>
        public int GetUserGroupId(string accessToken, string openid)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/getid?access_token={0}", accessToken);
            var data = new
            {
                openid = openid
            };
            var postData = data.ToJson();

            var groupId = -1;
            var result = JsonHelper<GroupJsonId>.ConvertJson(url, postData);
            if (result != null)
            {
                groupId = result.groupid;
            }
            return groupId;
        }



        /// <summary>
        /// 修改分组名  
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="id">分组id，由微信分配</param>
        /// <param name="name">分组名字（30个字符以内）</param>
        /// <returns></returns>
        public ErrorJsonResult UpdateGroupName(string accessToken, int id, string name)
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
            var postData = data.ToJson();

            return Helper.GetExecuteResult(url, postData);
        }



        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="openid">用户的OpenID</param>
        /// <param name="to_groupid">分组id</param>
        /// <returns></returns>
        public ErrorJsonResult MoveUserToGroup(string accessToken, string openid, int to_groupid)
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

        /// <summary>
        /// 批量移动用户分组
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="openid_list">用户唯一标识符openid的列表（size不能超过50）</param>
        /// <param name="to_groupid">分组id</param>
        /// <returns></returns>
        public ErrorJsonResult MoveListUserToGroup(string accessToken, List<string> openid_list, int to_groupid)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/members/batchupdate?access_token={0}",
                accessToken);
            var data = new
            {
                openid = openid_list,
                to_groupid = to_groupid
            };
            var postData = data.ToJson();
            return Helper.GetExecuteResult(url, postData);
        }

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="id">分组的id</param>
        /// <returns></returns>
        public ErrorJsonResult DeleteGroup(string access_token, int id)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/delete?access_token={0}", access_token);
            var data = new
            {
                group = new { id = id }
            };
            var postData = data.ToJson();
            return Helper.GetExecuteResult(url, postData);
        }

        #endregion
        #region 设置用户备注名

        /// <summary>
        /// 设置备注名
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="openid">用户标识</param>
        /// <param name="remark">新的备注名，长度必须小于30字符</param>
        /// <returns></returns>
        public ErrorJsonResult UpdateUserRemark(string accessToken, string openid, string remark)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info/updateremark?access_token={0}",
                accessToken);
            var data = new
            {
                openid = openid,
                remark = remark
            };
            var postData = data.ToJson();
            return Helper.GetExecuteResult(url, postData);
        }
        #endregion
        #region 获取用户基本信息(UnionID机制)

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        public UserInfo GetUserDetail(string accessToken, string openId, Language lang = Language.zh_CN)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang={2}",
                   accessToken, openId, lang.ToString());
            var result = JsonHelper<UserJson>.ConvertJson(url);
            return new UserInfo(result);
        }

        /// <summary>
        /// 批量获取用户基本信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<UserInfo> GetListUserDetail(string accessToken, List<GetListUserJson> list)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token={0}", accessToken);
            var data = new
            {
                user_list = list
            };
            var listUserInfo = new List<UserInfo>();
            var postData = data.ToJson();
            var result = JsonHelper<UserDetailListJsonResult>.ConvertJson(url, postData);
            if (result != null && result.user_info_list != null)
            {
                listUserInfo.AddRange(result.user_info_list.Select(o => new UserInfo(o)));
            }
            return listUserInfo;
        }
        #endregion
        #region 获取用户列表

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
        #endregion
        ///---------------------------------------------------------------------------------------------

    }
}
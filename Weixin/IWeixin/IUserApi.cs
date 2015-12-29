using System.Collections.Generic;
using Weixin.Model;
using Weixin.Model.Common;
using Weixin.Model.Enum;
using Weixin.Weixin;

namespace Weixin.IWeixin
{
    /// <summary>
    /// 微信用户管理的API接口
    /// </summary>
    public interface IUserApi
    {
        /// <summary>
        /// 获取关注用户列表
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="nextOpenId">第一个拉取的OPENID，不填默认从头开始拉取</param>
        /// <returns></returns>
        List<string> GetUserList(string accessToken, string nextOpenId = null);

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        UserJson GetUserDetail(string accessToken, string openId,
            Language lang = Language.zh_CN);

        /// <summary>
        /// 查询所有分组
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <returns></returns>
        //List<GroupJson> GetGroupList(string accessToken);

        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="name">分组名称</param>
        /// <returns></returns>
        GroupJson CreateGroup(string accessToken, string name);

        /// <summary>
        /// 查询用户所在分组
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="openid">用户的OpenID</param>
        /// <returns></returns>
        int GetUserGroupId(string accessToken, string openid);

        /// <summary>
        /// 修改分组名
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="id">分组id，由微信分配</param>
        /// <param name="name">分组名字（30个字符以内）</param>
        /// <returns></returns>
        CommonResult UpdateGroupName(string accessToken, int id, string name);

        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="openid">用户的OpenID</param>
        /// <param name="to_groupid">分组id</param>
        /// <returns></returns>
        CommonResult MoveUserToGroup(string accessToken, string openid, int to_groupid);
    }
}

   
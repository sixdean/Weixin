using System.Collections.Generic;
using Weixin.DAL;
using Weixin.Model.Enum;

namespace Weixin.Model.Common
{
    /// <summary>
    /// 高级接口获取的用户信息。
    /// 在关注者与公众号产生消息交互后，公众号可获得关注者的OpenID
    /// （加密后的微信号，每个用户对每个公众号的OpenID是唯一的。对于不同公众号，同一用户的openid不同）。
    /// 公众号可通过本接口来根据OpenID获取用户基本信息，包括昵称、头像、性别、所在城市、语言和关注时间。
    /// </summary>
    public class UserJson
    {
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        public string subscribe { get; set; }

        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 用户的昵称
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        public string language { get; set; }

        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 用户所在省份
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
        /// </summary>
        public string headimgurl { get; set; }

        /// <summary>
        /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        public string subscribe_time { get; set; }

        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段
        /// </summary>
        public string unionid { get; set; }

        /// <summary>
        /// 公众号运营者对粉丝的备注，公众号运营者可在微信公众平台用户管理界面对粉丝添加备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 用户所在的分组ID
        /// </summary>
        public string groupid { get; set; }

    }
    /// <summary>
    /// 获取关注用户列表的Json结果
    /// </summary>
    public class UserListJsonResult
    {
        /// <summary>
        /// 关注该公众账号的总用户数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 拉取的OPENID个数，最大值为10000
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 列表数据，OPENID的列表
        /// </summary>
        public OpenIdListData data { get; set; }

        /// <summary>
        /// 拉取列表的后一个用户的OPENID
        /// </summary>
        public string next_openid { get; set; }
    }

    /// <summary>
    /// 批量获取用户基本信息返回json
    /// </summary>
    public class UserDetailListJsonResult
    {
        public List<UserJson> user_info_list { get; set; }
    }

    /// <summary>
    /// 列表数据，OPENID的列表
    /// </summary>
    public class OpenIdListData
    {
        /// <summary>
        /// OPENID的列表
        /// </summary>
        public List<string> openid { get; set; }
    }

    /// <summary>
    /// 分组信息
    /// </summary>
    public class GroupJson
    {
        /// <summary>
        /// 分组id，由微信分配
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 分组名字，UTF8编码
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 分组内用户数量
        /// </summary>
        public short count { get; set; }
    }

    /// <summary>
    /// 查询所有分组返回json
    /// </summary>
    public class GroupListJson
    {
        public List<GroupJson> groups { get; set; }
    }

    /// <summary>
    /// 创建分组返回结果json
    /// </summary>
    public class CreateGroupJson
    {
        public GroupJson group { get; set; }
    }
    /// <summary>
    ///  查询用户所在分组 返回结果json
    /// </summary>
    public class GroupJsonId
    {
        public int groupid { get; set; }
    }

    /// <summary>
    /// 批量获取用户基本信息参数json
    /// </summary>
    public class GetListUserJson
    {
        public Language _lang = Language.zh_CN;
        public string openid { get; set; }

        public Language lang
        {
            get { return _lang; }
            set { _lang = value; }
        }
    }
}
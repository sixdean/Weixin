using System;
using Weixin.Model.Common;

namespace Weixin.DAL
{
    partial class WeixinDataContext
    {
    }

    partial class SysUser
    {
    }

    partial class Menu
    {
    }

    partial class GroupInfo
    {
        public GroupInfo(GroupJson groupJson)
        {
            id = Guid.NewGuid().ToString();
            _groupId = groupJson.id;
            _name = groupJson.name;
            _count = groupJson.count;
            _IsSync = 1;
            _IsAdd = 0;
            _IsUpdate = 0;
            _IsDelete = 0;
        }

        partial void OnCreated()
        {
            _IsSync = 0;
            _IsAdd = 0;
            _IsUpdate = 0;
            _IsDelete = 0;
            _count = 0;
        }
    }

    partial class UserInfo
    {
        public UserInfo(UserJson userJson)
        {
            _Id = Guid.NewGuid().ToString();
            _openid = userJson.openid;
            _subscribe = userJson.subscribe;
            _nickname = userJson.nickname;
            _sex = userJson.sex;
            _city = userJson.city;
            _country = userJson.country;
            _province = userJson.province;
            _language = userJson.language;
            _headimgurl = userJson.headimgurl;
            _subscribe_time = userJson.subscribe_time;
            _unionid = userJson.unionid;
            _remark = userJson.remark;
            _groupid = userJson.groupid;
        }

        public UserInfo(string openid)
        {
            _Id = Guid.NewGuid().ToString();
            _openid = openid;
        }
    }
}
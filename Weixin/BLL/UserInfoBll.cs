using System.Collections.Generic;
using System.Linq;
using Weixin.DAL;
using Weixin.Model.Common;

namespace Weixin.BLL
{
    public class UserInfoBll : BaseBll<UserInfo>
    {
        public override UserInfo GetById(string id)
        {
            return DataContext.UserInfo.FirstOrDefault(o => o.openid == id);
        }

        /// <summary>
        ///     根据用户分组id获取用户列表信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IQueryable<UserInfo> GetUserInfosByGroupId(string groupId)
        {
            return DataContext.UserInfo.Where(o => (string.IsNullOrEmpty(groupId) || o.groupid == groupId));
        }


        /// <summary>
        ///     获取需要同步信息的用户openid
        /// </summary>
        /// <param name="groupId">分组id</param>
        /// <returns></returns>
        public IQueryable<GetListUserJson> GetNeedSyncUserOpenidList(string groupId = null)
        {
            var obj = from a in DataContext.UserInfo
                      where (string.IsNullOrEmpty(groupId) || a.groupid == groupId)
                      select new GetListUserJson
                      {
                          openid = a.openid
                      };
            return obj;
        }

        /// <summary>
        ///     根据从微信服务器上获取的userjson数据更新本地数据库userinfo;
        /// </summary>
        /// <param name="list"></param>
        public void UpdateUserInfoByUserJson(List<UserJson> list)
        {
            foreach (var userJson in list)
            {
                var userinfo = GetById(userJson.openid);
                if (userinfo != null)
                {
                    userinfo.unionid = userJson.unionid;
                    userinfo.subscribe = userJson.subscribe;
                    if (userinfo.subscribe != "0")
                    {
                        userinfo.nickname = userJson.nickname;
                        userinfo.sex = userJson.sex;
                        userinfo.language = userJson.language;
                        userinfo.country = userJson.country;
                        userinfo.province = userJson.province;
                        userinfo.city = userJson.city;
                        userinfo.headimgurl = userJson.headimgurl;
                        userinfo.subscribe_time = userJson.subscribe_time;
                        userinfo.remark = userJson.remark;
                        userinfo.groupid = userJson.groupid;
                    }
                    DataContext.SubmitChanges();
                }
            }
        }
    }
}
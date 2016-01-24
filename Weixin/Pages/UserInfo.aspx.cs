using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.DynamicData;
using System.Web.Services;
using Newtonsoft.Json;
using Weixin.BLL;
using Weixin.BLL.Weixin;
using Weixin.DAL;
using Weixin.Model.Common;

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
            var result = "ok";
            var msg = "";
            try
            {
                var userApi = new UserApi();
                var groupList = userApi.GetGroupList(accessToken);
                var groupBll = new GroupInfoBll();
                groupBll.DeleteAll<GroupInfo>();
                groupBll.AddListGroupInfo(groupList);
            }
            catch (Exception e)
            {
                result = "err";
                msg = e.Message;
            }
            return JsonConvert.SerializeObject(new AjaxResult(result, msg));
        }


        [WebMethod]
        public static string GetWeixinUserListData()
        {
            var result = "ok";
            var msg = "";
            try
            {
                var userApi = new UserApi();
                UserInfoBll userInfoBll = new UserInfoBll();
                var total = 0;
                var count = 0;
                var nextopenid = string.Empty;
                var userList = new List<DAL.UserInfo>();
                userInfoBll.DeleteAll<DAL.UserInfo>();
                do
                {
                    var userListJsonResult = userApi.GetUserList(accessToken, nextopenid);
                    if (userListJsonResult != null && userListJsonResult.data != null)
                    {
                        total = userListJsonResult.total;
                        count = userListJsonResult.count;
                        nextopenid = userListJsonResult.next_openid;
                        userList.AddRange(userListJsonResult.data.openid.Select(o => new DAL.UserInfo(o)));
                        userInfoBll.Add(userList);
                    }

                } while (total > count);
            }
            catch (Exception e)
            {
                result = "err";
                msg = e.Message;
            }
            return JsonConvert.SerializeObject(new AjaxResult(result, msg));
        }

        [WebMethod]
        public static string GetWeixinUserInfoData()
        {
            var result = "ok";
            var msg = "";
            try
            {
                var userApi = new UserApi();
                UserInfoBll userInfoBll = new UserInfoBll();
                var openList = userInfoBll.GetNeedSyncUserOpenidList();
                var startIndex = 0;
                do
                {
                    var list = openList.Take(100).Skip(startIndex).ToList();
                    var userDetailList = userApi.GetListUserDetail(accessToken, list);
                    userInfoBll.UpdateUserInfoByUserJson(userDetailList);
                    startIndex = startIndex + 100;
                } while (openList.Count() > startIndex + 100);
            }
            catch (Exception e)
            {
                result = "err";
                msg = e.Message;
            }
            return JsonConvert.SerializeObject(new AjaxResult(result, msg));
        }
        [WebMethod]
        public static string GroupFormSubmit(string d, string type)
        {
            var result = "ok";
            var msg = "";
            try
            {
                var groupInfoBll = new GroupInfoBll();
                var groupInfo = JsonConvert.DeserializeObject<GroupInfo>(d);
                groupInfo.IsSync = 0;             //同步状态修改为否
                switch (type)
                {
                    case "update":
                        groupInfo.IsUpdate = 1;
                        groupInfoBll.UpdateGroupInfo(groupInfo);
                        break;
                    case "add":
                        groupInfo.IsAdd = 1;
                        groupInfoBll.AddGroupInfo(groupInfo);
                        break;
                    case "delete":
                        groupInfo.IsDelete = 1;      //删除状态修改为是,逻辑删除
                        groupInfoBll.UpdateGroupInfo(groupInfo);
                        break;
                    default:
                        result = "err";
                        msg = "类型错误";
                        break;
                }
            }
            catch (Exception e)
            {
                result = "err";
                msg = e.Message;
            }
            var ss = new AjaxResult(result, msg);
            var a = JsonConvert.SerializeObject(ss);
            return a;
        }

        [WebMethod]
        public static string UpdateWeixinGroupData()
        {
            var result = "ok";
            var msg = "";
            try
            {
                GroupInfoBll giBll = new GroupInfoBll();
                UserApi userApi = new UserApi();
                //删除状态数据
                var deleteGroups = giBll.GetDeleteGroupInfos();
                foreach (var dGroup in deleteGroups)
                {
                    if (dGroup.groupId != null)
                    {
                        var dresult = userApi.DeleteGroup(accessToken, dGroup.groupId.Value);
                        if (dresult != null && dresult.ErrCode == 0)
                        {
                            giBll.Delete(dGroup);
                        }
                    }
                    else
                    {
                        giBll.Delete(dGroup);
                    }
                }
                //新增状态数据
                var addGroups = giBll.GetAddGroupInfos();
                foreach (var addGroup in addGroups)
                {
                    var aresult = userApi.CreateGroup(accessToken, addGroup.name);
                    if (aresult != null && aresult.id.ToString() != "")
                    {
                        addGroup.groupId = aresult.id;
                        addGroup.IsUpdate = 0;//即是新增又有修改的状态修改为未修改
                        addGroup.IsSync = 1;
                        giBll.DataContext.SubmitChanges();
                    }
                }
                //修改状态数据
                var updateGroups = giBll.GetUpdateGroupInfos();
                foreach (var updateGroup in updateGroups)
                {
                    if (updateGroup.groupId != null)
                    {
                        var uresult = userApi.UpdateGroupName(accessToken, updateGroup.groupId.Value, updateGroup.name);
                        if (uresult != null && uresult.ErrCode == 0 && uresult.ErrMsg == "ok")
                        {
                            updateGroup.IsSync = 1;
                            giBll.DataContext.SubmitChanges();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                result = "err";
                msg = e.Message;
            }
            return JsonConvert.SerializeObject(new AjaxResult(result, msg));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Weixin.DAL;

namespace Weixin.BLL
{
    public class GroupInfoBll : BaseBll<GroupInfo>
    {

        /// <summary>
        /// 根据id获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override GroupInfo GetById(string id)
        {
            return DataContext.GroupInfo.FirstOrDefault(o => o.id == id);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public IQueryable<GroupInfo> GetGouptInfos()
        {
            return DataContext.GroupInfo.OrderBy(o => o.groupId);
        }

        /// <summary>
        /// 单个新增
        /// </summary>
        /// <param name="groupInfo"></param>
        public void AddGroupInfo(GroupInfo groupInfo)
        {
            groupInfo.id = string.IsNullOrEmpty(groupInfo.id) ? CreateEntityId() : groupInfo.id;
            Add(groupInfo);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        public void AddListGroupInfo(List<GroupInfo> list)
        {
            foreach (var groupInfo in list)
            {
                groupInfo.id = string.IsNullOrEmpty(groupInfo.id) ? CreateEntityId() : groupInfo.id;
            }
            Add(list);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="groupInfo"></param>
        public void UpdateGroupInfo(GroupInfo groupInfo)
        {
            var dbEntity = GetById(groupInfo.id);
            UpdateDbEntity(dbEntity, groupInfo);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="groupInfo"></param>
        public void DeleteGroupInfo(GroupInfo groupInfo)
        {
            var dbEntity = GetById(groupInfo.id);
            Delete(dbEntity);
        }

        /// <summary>
        /// 未同步删除状态的数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<GroupInfo> GetDeleteGroupInfos()
        {
            return DataContext.GroupInfo.Where(o => o.IsSync == 0 && o.IsDelete == 1);
        }

        /// <summary>
        /// 未同步新增状态的数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<GroupInfo> GetAddGroupInfos()
        {
            return DataContext.GroupInfo.Where(o => o.IsSync == 0 && o.IsAdd == 1);
        }

        /// <summary>
        /// 未同步修改状态的数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<GroupInfo> GetUpdateGroupInfos()
        {
            return DataContext.GroupInfo.Where(o => o.IsSync == 0 && o.IsUpdate == 1);
        }
    }

}
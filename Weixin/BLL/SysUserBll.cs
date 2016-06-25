using System;
using System.Linq;
using System.Web.Configuration;
using System.Web.Security;
using Weixin.BLL.Common;
using Weixin.DAL;

namespace Weixin.BLL
{
    public class SysUserBll : BaseBll<SysUser>
    {
        #region 公共
        public override SysUser GetById(string id)
        {
            return DataContext.SysUser.FirstOrDefault(s => s.Id == id);
        }
        public void AddSysUser(SysUser sysUser)
        {
            sysUser.Id = CreateEntityId();
            sysUser.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(sysUser.Password,
                FormsAuthPasswordFormat.MD5.ToString());
            sysUser.CreateDate = DateTime.Now;
            sysUser.UpdateDate = DateTime.Now;
            Add(sysUser);
        }

        public bool CheckUserId(string userId)
        {
            return DataContext.SysUser.Any(s => s.UserId == userId);
        }
        public bool CheckLogin(string userId, string passWord)
        {
            return
                DataContext.SysUser.Any(
                    s =>
                        s.UserId == userId &&
                        s.Password == passWord.Md5());
        }
        #endregion
        #region 私有

        #endregion


    }
}
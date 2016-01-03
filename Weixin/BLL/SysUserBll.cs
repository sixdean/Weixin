using System;
using System.Linq;
using System.Web.Configuration;
using System.Web.Security;
using Weixin.DAL;

namespace Weixin.BLL
{
    public class SysUserBll : BaseBll
    {
        #region 公共
        public SysUser GetSysUserById(string id)
        {
            return DataContext.SysUser.FirstOrDefault(s => s.Id == id);
        }

        public void Add(SysUser sysUser)
        {
            sysUser.Id = CreateEntityID();
            sysUser.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(sysUser.Password,
                FormsAuthPasswordFormat.MD5.ToString());
            sysUser.CreateDate = DateTime.Now;
            sysUser.UpdateDate = DateTime.Now;
            this.Add(sysUser);
        }

        public bool CheckLogin(string name, string passWord)
        {
            return
                DataContext.SysUser.Any(
                    s =>
                        s.Name == name &&
                        s.Password ==
                        FormsAuthentication.HashPasswordForStoringInConfigFile(passWord, FormsAuthPasswordFormat.MD5.ToString()));
        }
        #endregion
        #region 私有

        #endregion
    }
}
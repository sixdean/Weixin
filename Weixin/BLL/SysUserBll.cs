using System;
using System.Linq;
using System.Web.Configuration;
using System.Web.Security;
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
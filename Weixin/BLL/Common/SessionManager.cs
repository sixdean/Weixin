using System.Web;
using Weixin.DAL;
using Weixin.Model.Common;

namespace Weixin.BLL.Common
{
    public class SessionManager : BaseBll
    {
        #region Property

        public UserProfiles CurUserProfiles
        {
            get
            {
                if (HttpContext.Current == null || HttpContext.Current.Session == null ||
                    HttpContext.Current.Session[SessionKeys.CurUserCurUserProfiles] == null)
                {
                    HttpContext.Current.Response.Redirect("~/Login.aspx");
                    return null;
                }
                var userProfiles = SecurityManager.GetUserProfiles(HttpContext.Current.User.Identity.Name);
                return userProfiles;
            }
            set
            {
                if (HttpContext.Current == null || HttpContext.Current.Session == null)
                {
                    throw new BusinessException("无法获得当前用户的Session对象");
                }
                HttpContext.Current.Session[SessionKeys.CurUserCurUserProfiles] = value;
            }
        }

        #endregion

        #region Constructors

        public SessionManager()
        {
        }

        public SessionManager(WeixinDataContext context)
            : base(context)
        {
        }

        #endregion
    }
}
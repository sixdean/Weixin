using System.Data.Linq;
using Weixin.DAL;
using Weixin.Model.Common;

namespace Weixin.BLL.Common
{
    public class SecurityManager : BaseBll
    {
        #region Constructors

        public SecurityManager()
            : base()
        {

        }

        public SecurityManager(WeixinDataContext context)
            : base(context)
        {

        }
        #endregion

        #region Methods

        public bool IsAuthorized()
        {
            return true;
        }

        public static UserProfiles GetUserProfiles(string userId)
        {
            var userProfiles = new UserProfiles();
            userProfiles.UserId = userId;
            return userProfiles;
        }
        #endregion
    }
}
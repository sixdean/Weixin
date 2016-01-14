using System.Linq;
using Weixin.DAL;

namespace Weixin.BLL
{
    public class UserInfoBll : BaseBll<UserInfo>
    {

        public override UserInfo GetById(string id)
        {
            return DataContext.UserInfo.FirstOrDefault(o => o.openid == id);
        }
    }
}
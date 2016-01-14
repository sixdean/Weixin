using System.Linq;
using Weixin.DAL;

namespace Weixin.BLL
{
    public class GroupInfoBll : BaseBll<GroupInfo>
    {

        public override GroupInfo GetById(string id)
        {
            return DataContext.GroupInfo.FirstOrDefault(o => o.id == id);
        }

        public IQueryable<GroupInfo> GetGouptInfos()
        {
            return DataContext.GroupInfo;
        }

    }

}
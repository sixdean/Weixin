using System.Linq;
using Weixin.DAL;

namespace Weixin.BLL
{
    public class SysMenuBll : BaseBll
    {
        public IQueryable<SysMenu> GetSysMenuChildById(string id)
        {
            return DataContext.SysMenu.Where(s => s.ParentId == id);
        }

    }
}
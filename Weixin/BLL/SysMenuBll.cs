using System;
using System.Linq;
using Weixin.DAL;

namespace Weixin.BLL
{
    public class SysMenuBll : BaseBll<SysMenu>
    {
        public IQueryable<SysMenu> GetSysMenuChildById(string id)
        {
            return DataContext.SysMenu.Where(s => s.ParentId == id);
        }

        public override SysMenu GetById(string id)
        {
            return DataContext.SysMenu.FirstOrDefault(s => s.id == id);
        }
    }
}
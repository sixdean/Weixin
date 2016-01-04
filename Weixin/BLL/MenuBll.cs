using System;
using System.Linq;
using Weixin.DAL;

namespace Weixin.BLL
{
    public class MenuBll : BaseBll<Menu>
    {

        public override Menu GetById(string id)
        {
            return DataContext.Menu.FirstOrDefault(m => m.Id == id);
        }

        public IQueryable<Menu> GetMenus()
        {
            return DataContext.Menu;
        }

        public void AddMenu(Menu menu)
        {
            menu.Id = CreateEntityId();
            menu.MenuId = CreateEntityId();
            menu.CreateDate = DateTime.Now;
            menu.UpdateDate = DateTime.Now;
            Add(menu);
        }

        public void UpdateMenu(Menu menu)
        {
            var dbEntity = GetById(menu.Id);
            UpdateDbEntity(dbEntity, menu);
        }
    }
}
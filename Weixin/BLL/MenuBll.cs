using System;
using System.Collections.Generic;
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
            menu.CreateUser = "66";
            menu.UpdateUser = "66";
            Add(menu);
        }

        public void AddListMenu(List<Menu> list)
        {
            foreach (var t in list)
            {
                t.Id = CreateEntityId();
                t.MenuId = CreateEntityId();
                t.CreateDate = DateTime.Now;
                t.UpdateDate = DateTime.Now;
                t.CreateUser = "66";
                t.UpdateUser = "66";
            }
            Add(list);
        }

        public void UpdateMenu(Menu menu)
        {
            var dbEntity = GetById(menu.Id);
            UpdateDbEntity(dbEntity, menu);
        }
    }
}
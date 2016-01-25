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

        public IQueryable<object> GetMenus()
        {
            var obj = from a in DataContext.Menu
                      join b in DataContext.Menu on a.ParentId equals b.MenuId into c
                      from d in c.DefaultIfEmpty()
                      select new
                      {
                          a.Id,
                          a.MenuId,
                          a.Name,
                          a.Key,
                          a.Media_id,
                          a.UpdateDate,
                          a.UpdateUser,
                          a.CreateUser,
                          a.CreateDate,
                          a.Type,
                          a.Sort,
                          a.Url,
                          ParentName = d.Name
                      };
            return obj;
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
            menu.UpdateDate = DateTime.Now;
            menu.UpdateUser = "77";
            UpdateDbEntity(dbEntity, menu);
        }

        public IQueryable<Menu> GetParentRows(string parentId)
        {
            return DataContext.Menu.Where(o => o.ParentId == parentId);
        }
    }
}
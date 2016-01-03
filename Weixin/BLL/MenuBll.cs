using System;
using System.Linq;
using Weixin.DAL;

namespace Weixin.BLL
{
    public class MenuBll : BaseBll
    {
        public Menu GetmenuById(string id)
        {
            return DataContext.Menu.FirstOrDefault(m => m.Id == id);
        }
        public IQueryable<Menu> GetMenus()
        {
            return DataContext.Menu;
        }

        public void Add(Menu menu)
        {
            menu.Id = CreateEntityID();
            menu.CreateDate = DateTime.Now;
            menu.UpdateDate = DateTime.Now;
            Add<Menu>(menu);
        }

        public void Update(Menu menu)
        {
            Update<Menu>(menu);
        }

        public void Delete(Menu menu)
        {
            Delete<Menu>(menu);
        }
    }
}
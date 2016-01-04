using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Weixin.BLL;
using Weixin.DAL;

namespace Weixin.Ashx
{
    /// <summary>
    /// GetData 的摘要说明
    /// </summary>
    public class GetData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var id = context.Request["id"] ?? "";
            var type = context.Request["type"] ?? "";
            string result = "";
            switch (type)
            {
                case "GetSysMenu":
                    {
                        SysMenuBll sysMenu = new SysMenuBll();
                        var list = sysMenu.GetSysMenuChildById(id);
                        result = JsonConvert.SerializeObject(list);

                    }
                    break;
                case "GetMenu":
                    {
                        var listMenu = FactoryBll<MenuBll>.Instance.GetMenus();
                        var obj = new { total = listMenu.Count(), rows = listMenu };
                        result = JsonConvert.SerializeObject(obj);
                    }               
                    break;
            }
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
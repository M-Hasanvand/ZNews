using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.FacadPatterns;

namespace ZNews.EndPoint.ViewComponents
{
    public class MenuSite:ViewComponent
    {
        private readonly IMenuFacadForSite _menuFacadForSite;
        public MenuSite(IMenuFacadForSite menuFacadForSite)
        {
            _menuFacadForSite = menuFacadForSite;
        }
        public IViewComponentResult Invoke()
        {
            var menus = _menuFacadForSite.GetMenusForSiteService.Execute().Data;
            return View("MenuSite",menus);
        }
    }
}

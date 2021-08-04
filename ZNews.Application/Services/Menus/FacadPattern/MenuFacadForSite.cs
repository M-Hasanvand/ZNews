using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.Menus.Queries.GetMenusForSite;

namespace ZNews.Application.Services.Menus.FacadPattern
{
    public class MenuFacadForSite:IMenuFacadForSite
    {
        private readonly IDataBaseContext _context;
        public MenuFacadForSite(IDataBaseContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Queries
        /// </summary>
        private GetMenusForSiteService _getMenusForSiteService;
        public GetMenusForSiteService GetMenusForSiteService => _getMenusForSiteService?? new GetMenusForSiteService(_context);
    }
}

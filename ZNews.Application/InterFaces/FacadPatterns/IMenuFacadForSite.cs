using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.Services.Menus.Queries.GetMenusForSite;

namespace ZNews.Application.InterFaces.FacadPatterns
{
    public interface IMenuFacadForSite
    {
        /// <summary>
        /// Queries
        /// </summary>
        GetMenusForSiteService GetMenusForSiteService { get; }
    }
}

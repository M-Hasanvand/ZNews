using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.Services.Menus.Commands.AddMenu;
using ZNews.Application.Services.Menus.Commands.RemoveChildMenu;
using ZNews.Application.Services.Menus.Commands.RemoveMenu;
using ZNews.Application.Services.Menus.Commands.StatusChange;
using ZNews.Application.Services.Menus.Commands.StatusChangeMenu;
using ZNews.Application.Services.Menus.Queries.GetChildMenusForAdmin;
using ZNews.Application.Services.Menus.Queries.GetChildMenusForUserActivity;
using ZNews.Application.Services.Menus.Queries.GetMenusForAdmin;
using ZNews.Application.Services.Menus.Queries.GetMenusForUserActivity;

namespace ZNews.Application.InterFaces.FacadPatterns
{
    public interface IMenusFacadForAdmin
    {
        /// <summary>
        /// Commands
        /// </summary>
        AddMenuService AddMenuService { get; }
        AddChildMenuService AddChildMenuService { get; }
        RemoveMenuService RemoveMenuService { get; }
        RemoveChildMenuService RemoveChildMenuService { get;  }
        StatusChangeMenuService StatusChangeMenuService { get; }
        StatusChangeChildMenuService StatusChangeChildMenuService { get; }

        /// <summary>
        /// Queries
        /// </summary>
        GetMenusForAdminService GetMenusForAdminService { get; }
        GetChildMenusForAdminService GetChildMenusForAdminService { get; }
        GetMenusForUserActivityService GetMenusForUserActivityService { get; }
        GetChildMenusForUserActivityService GetChildMenusForUserActivityService { get; }
    }
}

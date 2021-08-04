using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.Menus.Commands.AddMenu;
using ZNews.Application.Services.Menus.Commands.RemoveChildMenu;
using ZNews.Application.Services.Menus.Commands.RemoveMenu;
using ZNews.Application.Services.Menus.Commands.StatusChange;
using ZNews.Application.Services.Menus.Commands.StatusChangeMenu;
using ZNews.Application.Services.Menus.Queries.GetChildMenusForAdmin;
using ZNews.Application.Services.Menus.Queries.GetChildMenusForUserActivity;
using ZNews.Application.Services.Menus.Queries.GetMenusForAdmin;
using ZNews.Application.Services.Menus.Queries.GetMenusForUserActivity;

namespace ZNews.Application.Services.Menus.FacadPattern
{
    public class MenusFacadForAdmin : IMenusFacadForAdmin
    {
        private readonly IDataBaseContext _context;
        public MenusFacadForAdmin(IDataBaseContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Commands
        /// </summary>
        private AddMenuService _addMenuService;
        public AddMenuService AddMenuService => _addMenuService??new AddMenuService(_context);

        private RemoveMenuService _removeMenuService;
        public RemoveMenuService RemoveMenuService => _removeMenuService??new RemoveMenuService(_context);

        private StatusChangeMenuService _statusChangeMenuService;
        public StatusChangeMenuService StatusChangeMenuService =>_statusChangeMenuService??new StatusChangeMenuService(_context);
       
        private AddChildMenuService _addChildMenuService;
        public AddChildMenuService AddChildMenuService => _addChildMenuService ?? new AddChildMenuService(_context);
        
        private RemoveChildMenuService _removeChildMenuService;
        public RemoveChildMenuService RemoveChildMenuService => _removeChildMenuService ?? new RemoveChildMenuService(_context);
        
        private StatusChangeChildMenuService _statusChangeChildMenuService;
        public StatusChangeChildMenuService StatusChangeChildMenuService => _statusChangeChildMenuService ?? new StatusChangeChildMenuService(_context);

        /// <summary>
        /// Queries
        /// </summary>
        private GetMenusForAdminService _getMenusForAdminService { get; }
        public GetMenusForAdminService GetMenusForAdminService=> _getMenusForAdminService??new GetMenusForAdminService(_context);
       
        private GetChildMenusForAdminService _getChildMenusForAdminService { get; }
        public GetChildMenusForAdminService GetChildMenusForAdminService => _getChildMenusForAdminService ?? new GetChildMenusForAdminService(_context);
        
        private GetMenusForUserActivityService _getMenusForUserActivityService { get; }
        public GetMenusForUserActivityService GetMenusForUserActivityService => _getMenusForUserActivityService ?? new GetMenusForUserActivityService(_context);
   
        private GetChildMenusForUserActivityService _getChildMenusForUserActivityService { get; }
        public GetChildMenusForUserActivityService GetChildMenusForUserActivityService => _getChildMenusForUserActivityService ?? new GetChildMenusForUserActivityService(_context);
   
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.HomePageSite.Commands.AddViewsForSite;
using ZNews.Application.Services.HomePageSite.Queries.GetViewCountForAdmin;

namespace ZNews.Application.Services.HomePageSite.FacadPattern
{
    public class HomePageFacad: IHomePageFacad
    {
        private readonly IDataBaseContext _context;
        public HomePageFacad(IDataBaseContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        private AddViewsForSite _addViewsForSite;
        public AddViewsForSite AddViewsForSite => _addViewsForSite?? new AddViewsForSite(_context);
        /// <summary>
        /// Queries
        /// </summary>
        public GetViewCountForAdmin _getViewCountForAdmin;
        public GetViewCountForAdmin GetViewCountForAdmin => _getViewCountForAdmin?? new GetViewCountForAdmin(_context);
    }
}

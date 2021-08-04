using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.Services.HomePageSite.Commands.AddViewsForSite;
using ZNews.Application.Services.HomePageSite.Queries.GetViewCountForAdmin;

namespace ZNews.Application.InterFaces.FacadPatterns
{
    public interface IHomePageFacad
    {
        /// <summary>
        /// Commands
        /// </summary>
        AddViewsForSite AddViewsForSite { get; }

        /// <summary>
        /// Queries
        /// </summary>
        GetViewCountForAdmin GetViewCountForAdmin { get; }
    }
}

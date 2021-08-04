using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.Services.News.Queries.GetDailyNewsListForSite;
using ZNews.Application.Services.News.Queries.GetDetailsNewsForSite;
using ZNews.Application.Services.News.Queries.GetNewsForSliderSite;
using ZNews.Application.Services.News.Queries.GetNewsMostCommentedsForSite;
using ZNews.Application.Services.News.Queries.GetNewsSearchAndTagAndCategoryForSite;

namespace ZNews.Application.InterFaces.FacadPatterns
{
    public interface INewsFacadForSite
    {
        /// <summary>
        /// Queris
        /// </summary>
        GetDailyNewsListForSiteService GetDailyNewsListForSiteService { get; }
        GetNewsMostCommentedsForSiteService GetNewsMostCommentedsForSiteService { get; }
        GetDetailsNewsForSiteService GetDetailsNewsForSiteService { get; }
        GetNewsForSliderSiteService GetNewsForSliderSiteService { get; }
        GetNewsSearchAndTagAndCategoryForSiteService GetNewsSearchAndTagAndCategoryForSiteService { get; }
    }
}

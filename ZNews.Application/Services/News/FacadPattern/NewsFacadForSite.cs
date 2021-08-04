using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.News.Queries.GetDailyNewsListForSite;
using ZNews.Application.Services.News.Queries.GetDetailsNewsForSite;
using ZNews.Application.Services.News.Queries.GetNewsForSliderSite;
using ZNews.Application.Services.News.Queries.GetNewsMostCommentedsForSite;
using ZNews.Application.Services.News.Queries.GetNewsSearchAndTagAndCategoryForSite;

namespace ZNews.Application.Services.News.FacadPattern
{
    public class NewsFacadForSite : INewsFacadForSite
    {
        private readonly IDataBaseContext _context;
        public NewsFacadForSite(IDataBaseContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Queris
        /// </summary>
        private GetDailyNewsListForSiteService _getDailyNewsListForSiteService;
        public GetDailyNewsListForSiteService GetDailyNewsListForSiteService => _getDailyNewsListForSiteService?? new GetDailyNewsListForSiteService(_context);


        private GetDetailsNewsForSiteService _getDetailsNewsForSiteService;
        public GetDetailsNewsForSiteService GetDetailsNewsForSiteService => _getDetailsNewsForSiteService?? new GetDetailsNewsForSiteService(_context);


        private GetNewsForSliderSiteService _getNewsForSliderSiteService;
        public GetNewsForSliderSiteService GetNewsForSliderSiteService => _getNewsForSliderSiteService?? new GetNewsForSliderSiteService(_context);

        private GetNewsMostCommentedsForSiteService _getNewsMostCommentedsForSiteService;
        public GetNewsMostCommentedsForSiteService GetNewsMostCommentedsForSiteService => _getNewsMostCommentedsForSiteService?? new GetNewsMostCommentedsForSiteService(_context);

        private GetNewsSearchAndTagAndCategoryForSiteService _getNewsSearchAndTagAndCategoryForSiteService;
        public GetNewsSearchAndTagAndCategoryForSiteService GetNewsSearchAndTagAndCategoryForSiteService => _getNewsSearchAndTagAndCategoryForSiteService?? new GetNewsSearchAndTagAndCategoryForSiteService(_context);
    }
}

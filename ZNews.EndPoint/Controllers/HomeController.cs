using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.EndPoint.Models;

namespace ZNews.EndPoint.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomePageFacad _homePageFacad;
        private readonly INewsFacadForSite _newsFacadForSite;
        private readonly ITagFacad _tagFacad;

        public HomeController(ILogger<HomeController> logger,
            IHomePageFacad homePageFacad,
            INewsFacadForSite newsFacadForSite,
            ITagFacad tagFacad)
        {
            _logger = logger;
            _homePageFacad = homePageFacad;
            _newsFacadForSite = newsFacadForSite;
            _tagFacad = tagFacad;
        }

        public IActionResult Index()
        {
            _homePageFacad.AddViewsForSite.Execute();
            HomePageViewModel homePageViewModel = new HomePageViewModel()
            {
                ListSliders = _newsFacadForSite.GetNewsForSliderSiteService.Execute().Data,
                ListNewsDaily = _newsFacadForSite.GetDailyNewsListForSiteService.Execute().Data,
                ListNewsMostCommented=_newsFacadForSite.GetNewsMostCommentedsForSiteService.Execute().Data,
                ListTags = _tagFacad.GetTagsForSiteService.Execute().Data
            };
            return View(homePageViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

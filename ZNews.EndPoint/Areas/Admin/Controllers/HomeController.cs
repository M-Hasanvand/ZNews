using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZNews.Common.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Domain.Entities.Users;
using ZNews.EndPoint.Utilities;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.EndPoint.Areas.Admin.Models;

namespace ZNews.EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin,Reporter")]
    public class HomeController : Controller
    {
        private readonly IUserFacad _userFacad;
        private readonly IHomePageFacad _homePageFacad;
        public HomeController(IUserFacad userFacad, IHomePageFacad homePageFacad)
        {
            _userFacad = userFacad;
            _homePageFacad = homePageFacad;
        }
        public IActionResult Index()
        {
            ViewBag.UserId=ClaimUtility.UserId(HttpContext.User);
            ViewBag.UserGuid=_userFacad.GetUserPropertiesService.UserGuid(ClaimUtility.UserId(HttpContext.User));
            HomePageViewsForAdminViewModel homePageViewsForAdminViewModel = new HomePageViewsForAdminViewModel()
            {
                ToDay=_homePageFacad.GetViewCountForAdmin.ToDay().Data,
                All=_homePageFacad.GetViewCountForAdmin.All().Data
            };
            return View(homePageViewsForAdminViewModel);
        }
    }
}

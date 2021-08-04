using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.EndPoint.Areas.Admin.Models.User;
using ZNews.EndPoint.Utilities;

namespace ZNews.EndPoint.ViewComponents
{
    public class MenuUserPropertis: ViewComponent
    {
        private readonly IUserFacad _userFacad;
        public MenuUserPropertis(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }
        public  IViewComponentResult Invoke()
        {
            MenuUserPropertisViewModel menuUserPropertisViewModel = new MenuUserPropertisViewModel()
            {
                UserId=ClaimUtility.UserId(HttpContext.User),
                UserGuid = _userFacad.GetUserPropertiesService.UserGuid(ClaimUtility.UserId(HttpContext.User)),
                ImageUrl=_userFacad.GetUserPropertiesService.ImageUrl(ClaimUtility.UserId(HttpContext.User))
            };
            
            return View("MenuUserPropertis", menuUserPropertisViewModel);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.FacadPatterns;

namespace ZNews.EndPoint.Views.Shared.Components.ViewComponents
{
    public class NewsMostCommented:ViewComponent
    {
        private readonly INewsFacadForSite _newsFacadForSite;
        public NewsMostCommented(INewsFacadForSite newsFacadForSite)
        {
            _newsFacadForSite = newsFacadForSite;
        }
        public IViewComponentResult Invoke()
        {
            var model = _newsFacadForSite.GetNewsMostCommentedsForSiteService.Execute().Data;
            return View("NewsMostCommented",model);
        }
    }
}

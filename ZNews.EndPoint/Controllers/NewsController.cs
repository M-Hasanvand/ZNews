using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.Comments.Commands.AddNewCommentForSite;
using ZNews.Application.Services.News.Queries.GetNewsSearchAndTagAndCategoryForSite;
using ZNews.EndPoint.Models;

namespace ZNews.EndPoint.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsFacadForSite _newsFacadForSite;
        private readonly ICommentFacad _commentFacad;

        public NewsController(INewsFacadForSite newsFacadForSite,ICommentFacad commentFacad)
        {
            _newsFacadForSite = newsFacadForSite;
            _commentFacad = commentFacad;
        }
        public IActionResult Index(RequestGetNewsSearchAndTagAndCategoryForSiteDto request)
        {
            NewsSearchAndTagAndCategoryViewModel newsSearchAndTagAndCategoryViewModel = new NewsSearchAndTagAndCategoryViewModel()
            {
                Data= _newsFacadForSite.GetNewsSearchAndTagAndCategoryForSiteService.Execute(new RequestGetNewsSearchAndTagAndCategoryForSiteDto()
                {
                    CateId = request.CateId,
                    TagId = request.TagId,
                    CurrentPage = request.CurrentPage,
                    PageSize = request.PageSize,
                    SearchKey = request.SearchKey,
                    ChildMenuId = request.ChildMenuId
                }).Data
            };
            return View(newsSearchAndTagAndCategoryViewModel);
        }
        public IActionResult Detail(long NewsId)
        {
            NewsDetailForSiteViewModel newsDetailForSiteViewModel = new NewsDetailForSiteViewModel()
            {
                NewsDetails= _newsFacadForSite.GetDetailsNewsForSiteService.Execute(NewsId).Data,
                ListComment=_commentFacad.GetListCommentForSiteService.Execute(NewsId).Data,
            };
            return View(newsDetailForSiteViewModel);
        }

        [HttpPost]
        public IActionResult AddComment(RequestAddNewCommentForSiteDto request)
        {
            return Json(_commentFacad.AddNewCommentForSiteService.Execute(new RequestAddNewCommentForSiteDto()
            {
                NewsId=request.NewsId,
                ParentId = request.ParentId,
                Email = request.Email,
                FullName = request.FullName,
                Text = request.Text,
            }));
        }
    }
}

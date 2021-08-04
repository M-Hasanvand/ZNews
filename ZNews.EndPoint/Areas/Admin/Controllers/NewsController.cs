using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.News.Commands.AddNews;
using ZNews.Application.Services.News.Commands.EditNews;
using ZNews.Application.Services.News.Queries.GetNewsForAdmin;
using ZNews.EndPoint.Areas.Admin.Models.News;
using ZNews.EndPoint.Utilities;

namespace ZNews.EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class NewsController : Controller
    {
        private readonly ICategoryFacad _categoryFacad;
        private readonly ITagFacad _tagFacad;
        private readonly INewsFacadForAdmin _newsFacadForAdmin;
        private readonly ICommentFacad _commentFacad;
        public NewsController(ICategoryFacad categoryFacad, ITagFacad tagFacad, INewsFacadForAdmin newsFacadForAdmin, ICommentFacad commentFacad)
        {
            _categoryFacad = categoryFacad;
            _tagFacad = tagFacad;
            _newsFacadForAdmin = newsFacadForAdmin;
            _commentFacad = commentFacad;
        }
        #region [List]
        [HttpGet]
        public IActionResult List(RequestGetNewsForAdminDto request)
        {
            var result=_newsFacadForAdmin.GetNewsForAdminService.Execute(new RequestGetNewsForAdminDto()
            {
                CurrentPage = request.CurrentPage,
                PageSize = request.PageSize,
                SearchKey = request.SearchKey,
                ordering=request.ordering
            });
            return View(result.Data);
        }
        #endregion
        #region [Add]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = new SelectList(_categoryFacad.GetCategoriesForAddNewsService.Execute().Data, "Id", "Name");
            ViewBag.Tags = _tagFacad.GetTagsForAddNewsService.Execute().Data;
            return View();
        }
        [HttpPost]
        public IActionResult Add(RequestAddNewsDto request)
        {
            if (request.ImageUrl != null)
            {
                request.ImageUrl = Request.Form.Files[0];
            }
            
            return Json(_newsFacadForAdmin.AddNewsService.Execute(new RequestAddNewsDto()
            {
                Title = request.Title,
                CategoryId = request.CategoryId,
                ImageUrl = request.ImageUrl,
                ShortDescription = request.ShortDescription,
                Description = request.Description,
                Tags = request.Tags,
                UserId = ClaimUtility.UserId(HttpContext.User)
            }));
        }
        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload)
        {
            if (upload.Length <= 0) return null;
            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/News/Photos/", fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);
            }
            var url = $"{"/Images/News/Photos/"}{fileName}";
            return Json(new { uploaded = true, url });
        }
        #endregion
        #region [Detail]
        [HttpGet]
        public IActionResult Detail(long NewsId)
        {
            ViewBag.UserId = ClaimUtility.UserId(HttpContext.User);
            NewsDetailForAdminViewModel newsDetailForAdminViewModel = new NewsDetailForAdminViewModel()
            {
                NewsDetail = _newsFacadForAdmin.GetDetailsNewsForAdminService.Execute(NewsId).Data,
                ListComments=_commentFacad.GetListCommentForAdminService.Execute(NewsId).Data
            };
            return View(newsDetailForAdminViewModel);
        }
        #endregion
        #region [Remove]
        [HttpPost]
        public IActionResult Remove(long NewsId)
        {
            return Json(_newsFacadForAdmin.RemoveNewsService.Execute(NewsId));
        }
        #endregion
        #region [StatusChange]
        [HttpPost]
        public IActionResult StatusChange(long NewsId)
        {
            return Json(_newsFacadForAdmin.StatusChangeNewsService.Execute(NewsId));
        }
        #endregion
        #region [SliderChanges]
        [HttpPost]
        public IActionResult SliderChanges(long NewsId)
        {
            return Json(_newsFacadForAdmin.SliderChangesNewsServices.Execute(NewsId));
        }
        #endregion
        #region [Edit]
        [HttpGet]
        public IActionResult Edit(long NewsId)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(RequestEditNewsDto request)
        {
            return Json(_newsFacadForAdmin.EditNewsService.Execute(new RequestEditNewsDto()
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                CategoryId = request.CategoryId
            }));
        }
        #endregion
    }

}

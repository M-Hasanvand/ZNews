using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.Tags.Commands.AddTagForAdmin;

namespace ZNews.EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TagsController : Controller
    {
        private readonly ITagFacad _tagFacad;
        public TagsController(ITagFacad tagFacad)
        {
            _tagFacad = tagFacad;
        }
        #region [AddAndList]
        [HttpGet]
        public IActionResult AddAndList()
        {
            ViewBag.GetTags = _tagFacad.GetTagsForAdminService.Execute().Data;
            return View();
        }
        [HttpPost]
        public IActionResult Add(RequestAddTagDto request)
        {
            return Json(_tagFacad.AddTagService.Execute(new RequestAddTagDto()
            {
                Name=request.Name,
                UserId=request.UserId
            }));
        }
        #endregion
        #region [StatusChange]
        [HttpPost]
        public IActionResult StatusChange(long TagId)
        {
            return Json(_tagFacad.StatusChangeTagService.Execute(TagId));
        }
        #endregion
        #region [Remove]
        [HttpPost]
        public IActionResult Remove(long TagId)
        {
            return Json(_tagFacad.RemoveTagService.Execute(TagId));
        }
        #endregion
    }
}

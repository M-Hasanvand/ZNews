using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.Categories.Commands.AddCategoryForAdmin;

namespace ZNews.EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryFacad _categoryFacad;
        public CategoriesController(ICategoryFacad categoryFacad)
        {
            _categoryFacad = categoryFacad;
        }
        #region [AddAndList]
        [HttpGet]
        public IActionResult AddAndList()
        {
            ViewBag.GetCategories =_categoryFacad.GetCategoriesForAdminService.Execute().Data;
            return View();
        }
        [HttpPost]
        public IActionResult Add(RequestAddCategoryDto request)
        {
            return Json(_categoryFacad.AddCategoryService.Execute(new RequestAddCategoryDto()
            {
                Name = request.Name,
                UserId = request.UserId
            }));
        }
        #endregion
        #region [StatusChange]
        [HttpPost]
        public IActionResult StatusChange(long CategoryId)
        {
            return Json(_categoryFacad.StatusChangeCategoryService.Execute(CategoryId));
        }
        #endregion
        #region [Remove]
        [HttpPost]
        public IActionResult Remove(long CategoryId)
        {
            return Json(_categoryFacad.RemoveCategoryService.Execute(CategoryId));
        }
        #endregion
    }
}

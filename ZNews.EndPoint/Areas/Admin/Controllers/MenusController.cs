using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.Menus.Commands.AddMenu;

namespace ZNews.EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenusController : Controller
    {
        private readonly IMenusFacadForAdmin _menusFacad;
        private readonly ICategoryFacad _categoryFacad;
        private readonly ITagFacad _tagFacad;
        public MenusController(IMenusFacadForAdmin menusFacad, ICategoryFacad categoryFacad, ITagFacad tagFacad)
        {
            _menusFacad = menusFacad;
            _categoryFacad = categoryFacad;
            _tagFacad = tagFacad;
        }
        #region [ListMenu]
        public IActionResult ListMenu()
        {
            ViewBag.GetMenus=_menusFacad.GetMenusForAdminService.Execute().Data;
            return View("AddAndListMenu");
        }
        #endregion
        #region [ListChildMenu]
        public IActionResult ListChildMenu(long MenuId)
        {
            ViewBag.MenuId = MenuId;
            return View(_menusFacad.GetChildMenusForAdminService.Execute(MenuId).Data);
        }
        #endregion
        #region [AddMenu]
        [HttpPost]
        public IActionResult AddMenu(RequestAddMenuDto request)
        {
            return Json(_menusFacad.AddMenuService.Execute(new RequestAddMenuDto()
            {
                Name=request.Name,
                UserId=request.UserId
            }));
        }
        #endregion
        #region [AddChildMenu]
        [HttpGet]
        public IActionResult AddChildMenu(long MenuId)
        {
            ViewBag.MenuId = MenuId;
            ViewBag.Categories = _categoryFacad.GetCategoriesForAddMenuService.Execute().Data;
            ViewBag.Tags = _tagFacad.GetTagsForAddMenuService .Execute().Data;
            return View();
        }
        [HttpPost]
        public IActionResult AddChildMenu(RequestAddChildMenuDto request ,List<RequestCategoryMenuDto> CategoriesMenuDto)
        {
            return Json(_menusFacad.AddChildMenuService.Execute(new RequestAddChildMenuDto()
            {
                ParentId=request.ParentId,
                Name=request.Name,
                UserId=request.UserId,
                CategoriesMenuDto=request.CategoriesMenuDto,
                TagsMenuDto=request.TagsMenuDto
            }));
        }
        #endregion
        #region [EditMenu]
        [HttpPost]
        public IActionResult EditMenu(long MenuId)
        {
            return View();
        }
        #endregion
        #region [EditChildMenu]
        [HttpPost]
        public IActionResult EditChildMenu(long ChildMenuId)
        {
            return View();
        }
        #endregion
        #region [RemoveMenu]
        [HttpPost]
        public IActionResult RemoveMenu(long MenuId)
        {
            return Json(_menusFacad.RemoveMenuService.Execute(MenuId));
        }
        #endregion
        #region [RemoveChildMenu]
        [HttpPost]
        public IActionResult RemoveChildMenu(long ChildMenuId)
        {
            return Json(_menusFacad.RemoveChildMenuService.Execute(ChildMenuId));
        }
        #endregion
        #region [StatusChangeMenu]
        [HttpPost]
        public IActionResult StatusChangeMenu(long MenuId)
        {
            return Json(_menusFacad.StatusChangeMenuService.Execute(MenuId));
        }
        #endregion
        #region [StatusChangeChildMenu]
        [HttpPost]
        public IActionResult StatusChangeChildMenu(long ChildMenuId)
        {
            return Json(_menusFacad.StatusChangeChildMenuService.Execute(ChildMenuId));
        }
        #endregion
    }
}

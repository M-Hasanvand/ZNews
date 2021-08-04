using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.Users.Commands.EditProfileUser;
using ZNews.Application.Services.Users.Commands.RegisterNewUserForAdmin;
using ZNews.Application.Services.Users.Queries.GetUsers;
using ZNews.Common.Roles;
using ZNews.Domain.Entities.Users;
using ZNews.EndPoint.Areas.Admin.Models.User;
using ZNews.EndPoint.Utilities;

namespace ZNews.EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserFacad _userFacadForAdmin;
        private readonly ICategoryFacad _categoryFacad;
        private readonly ITagFacad _tagFacad;
        private readonly ICommentFacad _commentFacad;
        private readonly INewsFacadForAdmin _newsFacadForAdmin;
        private readonly IMenusFacadForAdmin _menusFacadForAdmin;
        public UsersController(
            IUserFacad userFacadForAdmin,
            ICategoryFacad categoryFacad,
            ITagFacad tagFacad,
            ICommentFacad commentFacad,
            INewsFacadForAdmin newsFacadForAdmin,
            IMenusFacadForAdmin menusFacadForAdmin)
        {
            _userFacadForAdmin = userFacadForAdmin;
            _categoryFacad = categoryFacad;
            _tagFacad = tagFacad;
            _commentFacad = commentFacad;
            _newsFacadForAdmin = newsFacadForAdmin;
            _menusFacadForAdmin = menusFacadForAdmin;
        }
        #region[ListUsers]
        [HttpGet]
        public IActionResult ListUsers(RequestGetUsersDto request)
        {
            return View(_userFacadForAdmin.GetUsersService.Execute(new RequestGetUsersDto()
            {
                SearchKey = request.SearchKey,
                CurrentPage = request.CurrentPage,
                PageSize = request.PageSize
            }).Data);
        }
        #endregion
        #region[AddUser]
        [Authorize(Roles =UserRole.Admin)]
        [HttpGet]
        public IActionResult AddUser()
        {
            ViewBag.Roles = _userFacadForAdmin.GetRolesService.Execute().Data;
            return View();
        }
        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public IActionResult AddUser(RequestRegisterUserForAdminDto request)
        {
            
            RequestRegisterUserForAdminDto registerUserForAdminDto = new RequestRegisterUserForAdminDto()
            {
                FullName = request.FullName,
                Email = request.Email,
                MobileNumber = request.MobileNumber,
                NationalNumber = request.NationalNumber,
                Password = request.Password,
                RePassword = request.RePassword,
                Gender=request.Gender,
                Address=request.Address,
                OneUserId=ClaimUtility.UserId(HttpContext.User),
                RolesDto = request.RolesDto
            };
            return Json(_userFacadForAdmin.RegisterUserForAdminService.Execute(registerUserForAdminDto));
        }
        #endregion
        #region [UserStatusCahnge]
        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public IActionResult UserStatusChange(long UserId)
        {
            return Json(_userFacadForAdmin.UserStatusChangeForAdminService.Execute(UserId));
        }
        #endregion
        #region [RemoveUser
        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public IActionResult RemoveUser(long UserId)
        {
            return Json(_userFacadForAdmin.RemoveUserForAdminService.Execute(UserId));
        }
        #endregion
        #region [ProfileUserForAdmin]
        [HttpGet]
        public IActionResult ProfileUserForAdmin(Guid UserGuid)
        {
            var Data = _userFacadForAdmin.GetProfileUserForAdminService.Execute(UserGuid).Data;
            return View(model: Data);
        }
        #endregion
        #region [ProfileUserForUser]
        [HttpGet]
        public IActionResult ProfileUserForUser(Guid UserGuid)
        {
            var Data = _userFacadForAdmin.GetProfileUserForAdminService.Execute(UserGuid).Data;
            return View(model: Data);
        }
        #endregion
        #region [EditProfileUser]
        [HttpPost]
        public IActionResult EditProfileUser(RequestEditProfileUserDto request)
        {
            if(Request.Form.Files.Count>0)
            {
                request.ImageUrl = Request.Form.Files[0];
            }
            var Data = _userFacadForAdmin.EditProfileUserService.Execute(new RequestEditProfileUserDto()
            {
                Id=request.Id,
                FullName = request.FullName,
                Email = request.Email,
                MobileNumber = request.MobileNumber,
                NationalNumber = request.NationalNumber,
                Address = request.Address,
                About = request.About,
                Age = request.Age,
                ImageUrl = request.ImageUrl,
            });
            return Json(Data);
        }
        #endregion
        #region [UserActivitiesForAdmin]
        public IActionResult UserActivities(long UserId)
        {
            UserActivitiesViewModel userActivitiesViewModel = new UserActivitiesViewModel(){};
            if(_categoryFacad.GetCategoriesForUserActivityService.Execute(UserId).IsSuccess==true)
            {
                userActivitiesViewModel.ListCategories = _categoryFacad.GetCategoriesForUserActivityService.Execute(UserId).Data;
            }
            else
            {
                ViewBag.MessageCategory = _categoryFacad.GetCategoriesForUserActivityService.Execute(UserId).Message;
            }
            if (_tagFacad.GetTagsForUserActivityService.Execute(UserId).IsSuccess == true)
            {
                userActivitiesViewModel.ListTags = _tagFacad.GetTagsForUserActivityService.Execute(UserId).Data;
            }
            else
            {
                ViewBag.MessageTag = _tagFacad.GetTagsForUserActivityService.Execute(UserId).Message;
            }
            if (_menusFacadForAdmin.GetMenusForUserActivityService.Execute(UserId).IsSuccess == true)
            {
                userActivitiesViewModel.ListMenus = _menusFacadForAdmin.GetMenusForUserActivityService.Execute(UserId).Data;
            }
            else
            {
                ViewBag.MessageMenu = _menusFacadForAdmin.GetMenusForUserActivityService.Execute(UserId).Message;
            }
            if (_menusFacadForAdmin.GetChildMenusForUserActivityService.Execute(UserId).IsSuccess == true)
            {
                userActivitiesViewModel.ListChildMenus = _menusFacadForAdmin.GetChildMenusForUserActivityService.Execute(UserId).Data;
            }
            else
            {
                ViewBag.MessageChildMenu = _menusFacadForAdmin.GetChildMenusForUserActivityService.Execute(UserId).Message;
            }
            if (_newsFacadForAdmin.GetNewsForUserActivityService.Execute(UserId).IsSuccess == true)
            {
                userActivitiesViewModel.ListNews = _newsFacadForAdmin.GetNewsForUserActivityService.Execute(UserId).Data;
            }
            else
            {
                ViewBag.MessageNews = _newsFacadForAdmin.GetNewsForUserActivityService.Execute(UserId).Message;
            }
            if (_userFacadForAdmin.GetUsersForUserActivityService.Execute(UserId).IsSuccess == true)
            {
                userActivitiesViewModel.ListUsers = _userFacadForAdmin.GetUsersForUserActivityService.Execute(UserId).Data;
            }
            else
            {
                ViewBag.MessageUser = _userFacadForAdmin.GetUsersForUserActivityService.Execute(UserId).Message;
            }
            if (_commentFacad.GetCommentsForUserActivityService.Execute(UserId).IsSuccess == true)
            {
                userActivitiesViewModel.ListComments = _commentFacad.GetCommentsForUserActivityService.Execute(UserId).Data;
            }
            else
            {
                ViewBag.MessageComment = _commentFacad.GetCommentsForUserActivityService.Execute(UserId).Message;
            }
            return View(userActivitiesViewModel);
        }
        #endregion
    }
}
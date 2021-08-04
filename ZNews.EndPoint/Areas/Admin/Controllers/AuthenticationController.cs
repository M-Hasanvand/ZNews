
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.Users.Commands.LoginUserForAdmin;

namespace ZNews.EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthenticationController : Controller
    {
        private readonly IUserFacad _userFacad;
        public AuthenticationController(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }
        #region [Sginin]
        [HttpGet]
        public IActionResult Sginin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Sginin(RequestLoginUserForAdminDto request)
        {
           var user= _userFacad.LoginUserForAdminService.Execute(new RequestLoginUserForAdminDto()
            {
               Email=request.Email,
               NationalNumber=request.NationalNumber,
               Password=request.Password
            });
            var claim = new List<Claim>();
            if(user.IsSuccess==true)
            {
                claim.Add(new Claim(ClaimTypes.NameIdentifier, user.Data.Id.ToString()));
                claim.Add(new Claim(ClaimTypes.Name, user.Data.FullName));
                claim.Add(new Claim(ClaimTypes.Email, user.Data.Email));
                foreach (var item in user.Data.Roles)
                {
                    claim.Add(new Claim(ClaimTypes.Role, item.Name));
                }
                var claimIdentity = new ClaimsIdentity(claim,CookieAuthenticationDefaults.AuthenticationScheme);
                var claimprincipal = new ClaimsPrincipal(claimIdentity);
                var properties = new AuthenticationProperties()
                {
                    ExpiresUtc = DateTime.UtcNow.AddDays(50),
                    IsPersistent = true
                };
                HttpContext.SignInAsync(claimprincipal,properties);
            }
            return Json(user); 
        }
        #endregion
        #region [Sginout]

        public IActionResult Sginout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Admin/Authentication/Sginin");
        }
        #endregion
    }
}

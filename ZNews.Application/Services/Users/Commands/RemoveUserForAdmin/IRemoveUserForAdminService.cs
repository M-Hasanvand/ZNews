using Microsoft.AspNetCore.Hosting;
using System;
using ZNews.Common.Extensions;
using System.Linq;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Users.Commands.RemoveUserForAdmin
{
    public interface IRemoveUserForAdminService
    {
        ResultDto Execute(long UserId);
    }
    public class RemoveUserForAdminService : IRemoveUserForAdminService
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public RemoveUserForAdminService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public ResultDto Execute(long UserId)
        {
            var user = _context.Users.Find(UserId);
            var userRole = _context.UserInRoles.Where(p => p.UserId == UserId).ToList();
            if (user == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کاربر مورد نظر یافت نشد"
                };
            }
            foreach (var item in userRole)
            {
                item.IsRemove = true;
                item.RemoveTime = DateTime.Now;
            }
            if(!string.IsNullOrWhiteSpace(user.ImageUrl))
            {
                user.ImageUrl = Extension.MoveFile(user.ImageUrl, @"RecycleBin\AdminImage\", _environment);
            }
            user.RemoveTime = DateTime.Now;
            user.IsRemove = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "ادمین مورد نظر حذف شد",
            };
        }
    }
}

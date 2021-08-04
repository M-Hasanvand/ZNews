using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Users.Commands.UserStatusChangeForAdmin
{
    public interface IUserStatusChangeForAdminService
    {
        ResultDto Execute(long UserId);
    }
    public class UserStatusChangeForAdminService:IUserStatusChangeForAdminService
    {
        private readonly IDataBaseContext _context;
        public UserStatusChangeForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long UserId)
        {
            var user = _context.Users.Find(UserId);
            if(user==null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کاربر مورد نظر یافت نشد"
                };
            }
            user.IsActive = user.IsActive==true?false:true;
            string status = user.IsActive == false ? "غیرفعال" : "فعال";
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message =$"ادمین مورد نظر{status} شد",
            };
        }
    }
}

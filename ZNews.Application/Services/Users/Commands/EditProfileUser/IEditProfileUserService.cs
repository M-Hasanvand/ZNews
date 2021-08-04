using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;
using ZNews.Common.Extensions;
using ZNews.Common.Roles;

namespace ZNews.Application.Services.Users.Commands.EditProfileUser
{
    public interface IEditProfileUserService
    {
        ResultDto Execute(RequestEditProfileUserDto request);
    }
    public class EditProfileUserService: IEditProfileUserService
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public EditProfileUserService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public ResultDto Execute(RequestEditProfileUserDto request)
        {
            var user = _context.Users.Find(request.Id);
            if (user == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "ادمین مورد نظر پیدا نشد"
                };
            }
            user.FullName = request.FullName;
            user.Email = request.Email;
            user.MobileNumber = request.MobileNumber;
            if(request.ImageUrl!=null)
            { 
            user.ImageUrl = Extension.UploadFile(request.ImageUrl,@"Images\AdminImage\",_environment);
            }
            user.Age = request.Age;
            user.About = request.About;
            user.Address = request.Address;
            user.NationalNumber = request.NationalNumber;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "ویرایش اطلاعات ادمین تایید شد",
            };
        }
    }
    public class RequestEditProfileUserDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public IFormFile ImageUrl { get; set; }
        public int Age { get; set; }
        public string About { get; set; }
        public string Address { get; set; }
        public string NationalNumber { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Users.Commands.LoginUserForAdmin
{
    public interface ILoginUserForAdminService
    {
        ResultDto<ResultLoginUserForAdminDto> Execute(RequestLoginUserForAdminDto request);
    }
    public class LoginUserForAdminService: ILoginUserForAdminService 
    {
        private readonly IDataBaseContext _context;
        public LoginUserForAdminService(IDataBaseContext context)
        {
            _context = context; 
        }
        public ResultDto<ResultLoginUserForAdminDto> Execute(RequestLoginUserForAdminDto request)
        {
            var user = _context.Users.Where(p => p.Email == request.Email).FirstOrDefault();
            if(user==null)
            {
                return new ResultDto<ResultLoginUserForAdminDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "ایمیل معتبر نیست"
                };
            }
            if(user.IsActive==false)
            {
                return new ResultDto<ResultLoginUserForAdminDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "چنین ادمینی وجود ندارد"
                };
            }
            PasswordHasher passwordHasher = new PasswordHasher();
            bool SuccessPassword = passwordHasher.VerifyPassword(user.Password, request.Password);
            if(SuccessPassword == false)
            {
                return new ResultDto<ResultLoginUserForAdminDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "پسورد اشتباه است"
                };
            }
            if(user.NationalNumber!=request.NationalNumber)
            {
                return new ResultDto<ResultLoginUserForAdminDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "شماره ملی  اشتباه است "
                };
            }
            return new ResultDto<ResultLoginUserForAdminDto>()
            {
                Data = new ResultLoginUserForAdminDto()
                {
                    Id=user.Id,
                    UserGuid=user.UserGuid,
                    Email=user.Email,
                    FullName=user.FullName,
                    ImageUrl=user.ImageUrl,
                    IsOwner =user.IsOwner,
                    Roles= GetUserRoles(user.Id)
                },
                IsSuccess = true,
                Message = "ورود شما با موفقیت انجام شد"
            };
            List<RolesLoginUserForAdminDto> GetUserRoles(long UserId)
            {
                return _context.UserInRoles.Where(p => p.UserId == UserId).Select(p => new RolesLoginUserForAdminDto()
                {
                    Id = p.Id,
                    Name=p.Role.Name
                }).ToList();
            }
        }
    }
    public class RequestLoginUserForAdminDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NationalNumber { get; set; }
    }
    public class ResultLoginUserForAdminDto
    {
        public long Id { get; set; }
        public Guid UserGuid { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public bool IsOwner { get; set; }
        public List<RolesLoginUserForAdminDto> Roles { get; set; }
    }
    public class RolesLoginUserForAdminDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}

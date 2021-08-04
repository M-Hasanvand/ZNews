
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ZNews.Application.InterFaces.Context;
using ZNews.Common;
using ZNews.Common.Dto;
using ZNews.Domain.Entities.Users;

namespace ZNews.Application.Services.Users.Commands.RegisterNewUserForAdmin
{
    public interface IRegisterUserForAdminService
    {
        ResultDto Execute(RequestRegisterUserForAdminDto request);
    }
    public class RegisterUserForAdminService : IRegisterUserForAdminService
    {
        private readonly IDataBaseContext _context;
        public RegisterUserForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestRegisterUserForAdminDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.FullName))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "نام کاربر را وارد کنید"
                    };
                }
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "ایمیل را وارد کنید"
                    };
                }
                if (string.IsNullOrWhiteSpace(request.MobileNumber))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "شماره موبایل را وارد کنید"
                    };
                }
                if (string.IsNullOrWhiteSpace(request.NationalNumber))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "شماره ملی را وارد کنید"
                    };
                }
                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = " پسورد را وارد کنید"
                    };
                }
                if (string.IsNullOrWhiteSpace(request.RePassword))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = " تکرار پسورد را وارد کنید"
                    };
                }
                if (request.RePassword != request.Password)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "پسورد و تکرار ان با هم مطابقت ندارند"
                    };
                }
                if (request.RolesDto.Count < 1)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "سطح دسترسی ادمین را تعیین کنید"
                    };
                }
                ///-------{Add-User}--------------------------------------------------------------|
                User user = new User();
                user.FullName = request.FullName;
                user.MobileNumber = request.MobileNumber;
                user.IsActive = true;
                user.Address = request.Address==""?" ": request.Address;
                user.Gender = request.Gender;
                user.UserGuid = new Guid();

                var onUser = _context.Users.Find(request.OneUserId);
                if (onUser != null)
                {
                    user.OneUser = onUser;
                    user.OneUserId = request.OneUserId;
                }
                ///---------------------------------Email--------------------------------------|
                string PatternEmail = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";
                var match = Regex.Match(request.Email, PatternEmail, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "ایمیل  را درست وارد کنید"
                    };
                }
                if (_context.Users.Where(p => p.Email == request.Email).Any())
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "این ایمیل قبلا در سایت ثبت شده"
                    };
                }
                user.Email = request.Email;
                user.NationalNumber = request.NationalNumber;
                ///-------PasswordHasher-------------------------------------------------------------|
                PasswordHasher passwordHasher = new PasswordHasher();
                user.Password = passwordHasher.HashPassword(request.Password);
                ///-------{Add-Role}--------------------------------------------------------------|
                List<UserInRole> userInRoles = new List<UserInRole>();
                foreach (var item in request.RolesDto)
                {
                    var role = _context.Roles.Find(item.Id);
                    userInRoles.Add(new UserInRole()
                    {
                        Role = role,
                        RoleId = role.Id,
                        User = user,
                        UserId = user.Id
                    });
                }
                _context.UserInRoles.AddRange(userInRoles);
                _context.Users.Add(user);
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "ثبت نام ادمین با موفقیت انجام شد"
                };
            }
            catch (Exception)
            {

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطایی رخ داد لطفا دوباره سعی کنید"
                };
            }
        }
    }
    public class RequestRegisterUserForAdminDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string MobileNumber { get; set; }
        public string NationalNumber { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public int Age { get; set; }
        public long? OneUserId { get; set; }

        public List<RegisterUserRoleDto> RolesDto { get; set; } = new List<RegisterUserRoleDto>();
    }
    public class RegisterUserRoleDto
    {
        public long Id { get; set; }
    }
}

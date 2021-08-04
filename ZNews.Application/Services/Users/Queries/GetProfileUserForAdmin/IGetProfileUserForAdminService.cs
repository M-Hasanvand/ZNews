using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Users.Queries.GetProfileUserForAdmin
{
    public interface IGetProfileUserForAdminService
    {
        ResultDto<ResultProfileUserForAdminDto> Execute(Guid UserGuid);
    }
    public class GetProfileUserForAdminService :IGetProfileUserForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetProfileUserForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultProfileUserForAdminDto> Execute(Guid UserGuid)
        {
            var user = _context.Users.Where(p=>p.UserGuid== UserGuid).FirstOrDefault();
            if(user==null)
            {
                return new ResultDto<ResultProfileUserForAdminDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "ادمین مورد نظر پیدا نشد"
                };
            }

            return new ResultDto<ResultProfileUserForAdminDto>()
            {
                Data = new ResultProfileUserForAdminDto()
                {
                    Id = user.Id,
                    UserGuid=user.UserGuid,
                    FullName = user.FullName,
                    Email = user.Email,
                    MobileNumber = user.MobileNumber,
                    ImageUrl = user.ImageUrl,
                    Age=user.Age,
                    IsActive = user.IsActive,
                    About = user.About,
                    Gender=user.Gender,
                    Address=user.Address,
                    InsertTime = user.InsertTime,
                    NationalNumber = user.NationalNumber,
                    Roles = GetUserRoles(user.Id)
                },
                IsSuccess = true,
            };
            List<ResultProfileUserRoles> GetUserRoles(long UserId)
            {
                return _context.UserInRoles.Where(p => p.UserId == UserId).Select(p => new ResultProfileUserRoles()
                {
                    Id = p.Id,
                    DisplayName = p.Role.DisplayName,
                    Name=p.Role.Name
                }).ToList();
            }
        }
    }
    public class ResultProfileUserForAdminDto
    {
        public long Id { get; set; }
        public Guid UserGuid { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string ImageUrl { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public bool Gender { get; set; }
        public string About { get; set; }
        public string Address { get; set; }
        public string NationalNumber { get; set; }
        public DateTime InsertTime { get; set; }
        public virtual ICollection<ResultProfileUserRoles> Roles { get; set; } = new List<ResultProfileUserRoles>();
    }
    public class ResultProfileUserRoles
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
    }
}

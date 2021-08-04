using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using Microsoft.EntityFrameworkCore;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Users.Queries.GetUsersForUserActivity
{
    public interface IGetUsersForUserActivityService
    {
        ResultDto<List<ResultGetUsersForUserActivityDto>> Execute(long UserId);
    }
    public class GetUsersForUserActivityService : IGetUsersForUserActivityService
    {
        private readonly IDataBaseContext _context;
        public GetUsersForUserActivityService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultGetUsersForUserActivityDto>> Execute(long UserId)
        {
            if (UserId == 0)
            {
                return new ResultDto<List<ResultGetUsersForUserActivityDto>>()
                {
                    IsSuccess = false,
                    Message = "ایدی ادمین ارسال نشد"
                };
            }
            var users = _context.Users.Where(p => p.OneUserId == UserId).Select(p => new ResultGetUsersForUserActivityDto
            {
                Id = p.Id,
                UserGuid = p.UserGuid,
                FullName = p.FullName,
                Email = p.Email,
                ImageUrl = p.ImageUrl,
                InsertTime=p.InsertTime,
                IsActive = p.IsActive,
                IsOwner = p.IsOwner,
                RoleDto = _context.UserInRoles.Where(r => r.UserId == p.Id).Select(rp => new ResultRoleForUserActivitiesDto()
                {
                    Id = rp.Id,
                    DisplayName = rp.Role.DisplayName
                }).ToList(),
            }).OrderByDescending(p=>p.InsertTime).ToList();
            if (users.Count == 0)
            {
                return new ResultDto<List<ResultGetUsersForUserActivityDto>>()
                {
                    IsSuccess = false,
                    Message = " ادمینی اضافه نشده"
                };
            }
            return new ResultDto<List<ResultGetUsersForUserActivityDto>>()
            {
                Data = users,
                IsSuccess = true
            };
        }
    }

    #region [UsersDto]
    public class ResultGetUsersForUserActivityDto
    {
        public long Id { get; set; }
        public Guid UserGuid { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsOwner { get; set; }
        public DateTime InsertTime { get; set; }
        public List<ResultRoleForUserActivitiesDto> RoleDto { get; set; }
    }
    public class ResultRoleForUserActivitiesDto
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.Linq;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;
using ZNews.Common.Extensions;

namespace ZNews.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersService: IGetUsersService
    {
        private readonly IDataBaseContext _context;
        public GetUsersService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultPagingUserDto> Execute(RequestGetUsersDto request)
        {
            var query = _context.Users.AsQueryable();
            if(!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                query = query.Where(p => p.FullName.Contains(request.SearchKey) || p.Email.Contains(request.SearchKey));
            }
            var users = query.Pagenation(page:request.CurrentPage, pagesize:request.PageSize)
                .Select(p=>new GetUsersDto
                {
                    Id=p.Id,
                    UserGuid=p.UserGuid,
                    FullName=p.FullName,
                    Email=p.Email,
                    ImageUrl=p.ImageUrl,
                    IsActive=p.IsActive,
                    IsOwner=p.IsOwner,
                    RoleDto=GetUserRoles(p.Id)
                }).OrderByDescending(p => p.Id).ToList();
            return new ResultDto<ResultPagingUserDto>()
            {
                Data=new ResultPagingUserDto()
                {
                    Users=users,
                    CurrentPage=request.CurrentPage,
                    PageSize=request.PageSize,
                    UserCount= query.Count()
                },
                IsSuccess=true
            };
        }
        private List<RoleDto> GetUserRoles(long UserId)
        {
            return _context.UserInRoles.Where(p => p.UserId == UserId).Select(p => new RoleDto()
            {
                Id=p.Id,
                DisplayName=p.Role.DisplayName
            }).ToList();
        }
    }
}

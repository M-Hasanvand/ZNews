using System;
using System.Collections.Generic;
using System.Linq;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Users.Queries.GetRoles
{
    public class GetRolesService: IGetRolesService
    {
        private readonly IDataBaseContext _context;
        public GetRolesService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultRoles>> Execute()
        {
            var roles = _context.Roles.Select(p => new ResultRoles()
            {
                Id=p.Id,
                Name=p.Name,
                DisplayName=p.DisplayName
            }).ToList();
            return new ResultDto<List<ResultRoles>>()
            {
                Data= roles,
                IsSuccess=true,
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Menus.Queries.GetMenusForAdmin
{
    public interface IGetMenusForSiteService
    {
        ResultDto<List<ResultGetMenusForAdminDto>> Execute();

    }

    public class GetMenusForAdminService : IGetMenusForSiteService
    {
        private readonly IDataBaseContext _context;
        public GetMenusForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetMenusForAdminDto>> Execute()
        {
            var menus = _context.Menus.Include(c => c.Children).Select(p => new ResultGetMenusForAdminDto()
            {
                Id = p.Id,
                Name = p.Name,
                IsActive = p.IsActive,
                ChildCount = _context.ChildMenus.Where(ch=>ch.ParentId==p.Id).Count()
            }).OrderByDescending(p => p.Id).ToList();
            return new ResultDto<List<ResultGetMenusForAdminDto>>()
            {
                Data = menus,
                IsSuccess = true,
            };
        }
        
    }
    public class ResultGetMenusForAdminDto
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public long ChildCount { get; set; }
    }
}

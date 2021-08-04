using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Menus.Queries.GetChildMenusForAdmin
{
    public interface IGetChildMenusForAdminService
    {
        ResultDto<List<ResultGetChildMenusForAdminDto>> Execute(long MenuId);
    }
    public class GetChildMenusForAdminService : IGetChildMenusForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetChildMenusForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetChildMenusForAdminDto>> Execute(long MenuId)
        {
            var menu = _context.Menus.Find(MenuId);
            var menusChild = _context.ChildMenus
                .Where(w => w.ParentId == MenuId).Select(p => new ResultGetChildMenusForAdminDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsActive = p.IsActive,
                    ParentName = menu.Name,
                    ResultCategories = p.ChildMenu_Categories.Where(c => c.ChildMenuId == p.Id).Select(c => new ResultGetChildMenus_CategoryForAdminDto()
                    {
                        Id = c.Category.Id,
                        Name = c.Category.Name
                    }).ToList(),
                    ResultTags = p.ChildMenu_Tags.Where(ct => ct.ChildMenuId == p.Id).Select(ct => new ResultGetChildMenus_TagsForAdminDto() 
                    { 
                    Id=ct.Tag.Id,
                    Name=ct.Tag.Name
                    }).OrderByDescending(p=>p.Id).ToList()
                }).ToList();
            return new ResultDto<List<ResultGetChildMenusForAdminDto>>()
            {
                Data = menusChild,
                IsSuccess = true,
            };
        }
        
    }
    public class ResultGetChildMenusForAdminDto
    {
        public long Id { get; set; }
        public string ParentName { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public List<ResultGetChildMenus_CategoryForAdminDto> ResultCategories { get; set; }
        public List<ResultGetChildMenus_TagsForAdminDto> ResultTags { get; set; }
    }
    public class ResultGetChildMenus_CategoryForAdminDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
    public class ResultGetChildMenus_TagsForAdminDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Menus.Queries.GetMenusForSite
{
    public interface IGetMenusForSiteService
    {
        ResultDto<List<ResultGetMenusForSiteDto>> Execute();
    }

    public class GetMenusForSiteService : IGetMenusForSiteService
    {
        private readonly IDataBaseContext _context;
        public GetMenusForSiteService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetMenusForSiteDto>> Execute()
        {
            var menus = _context.Menus.Include(c => c.Children).Where(p => p.IsActive == true)
                .Select(p => new ResultGetMenusForSiteDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ChildMenus = p.Children.Where(c => c.ParentId == p.Id && c.IsActive == true).Select(c => new ResultGetChildMenusForSiteDto()
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList(),
                }).OrderBy(p => p.Id).ToList();
            return new ResultDto<List<ResultGetMenusForSiteDto>>()
            {
                Data = menus,
                IsSuccess = true,
            };
        }
    }
    public class ResultGetMenusForSiteDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual List<ResultGetChildMenusForSiteDto> ChildMenus { get; set; }
    }
    public class ResultGetChildMenusForSiteDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}

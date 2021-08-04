using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Menus.Queries.GetChildMenusForUserActivity
{
    public interface IGetChildMenusForUserActivityService
    {
        ResultDto<List<ResultGetChildMenusForUserActivityDto>> Execute(long UserId);
    }
    public class GetChildMenusForUserActivityService : IGetChildMenusForUserActivityService
    {
        private readonly IDataBaseContext _context;
        public GetChildMenusForUserActivityService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetChildMenusForUserActivityDto>> Execute(long UserId)
        {
            if (UserId == 0)
            {
                return new ResultDto<List<ResultGetChildMenusForUserActivityDto>>()
                {
                    IsSuccess = false,
                    Message = "ایدی ادمین ارسال نشد"
                };
            }
            var menusChild = _context.ChildMenus.Include(p=>p.Menu).Where(p=>p.UserId==UserId).Select(p => new ResultGetChildMenusForUserActivityDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsActive = p.IsActive,
                    InsertTime=p.InsertTime,
                    ParentName=p.Menu.Name,
                    ResultCategories = p.ChildMenu_Categories.Where(c => c.ChildMenuId == p.Id).Select(c => new ResultGetChildMenus_CategoryForUserActivityDto()
                    {
                        Id = c.Category.Id,
                        Name = c.Category.Name
                    }).ToList(),
                    ResultTags = p.ChildMenu_Tags.Where(ct => ct.ChildMenuId == p.Id).Select(ct => new ResultGetChildMenus_TagsForUserActivityDto()
                    {
                        Id = ct.Tag.Id,
                        Name = ct.Tag.Name
                    }).ToList()
                }).OrderByDescending(p => p.InsertTime).ToList();
            if (menusChild.Count == 0)
            {
                return new ResultDto<List<ResultGetChildMenusForUserActivityDto>>()
                {
                    IsSuccess = false,
                    Message = " زیر منویی ثبت نشده"
                };
            }
            return new ResultDto<List<ResultGetChildMenusForUserActivityDto>>()
            {
                Data=menusChild,
                IsSuccess=true
            };
        }
    }
    #region [ChildMenusDto]
    public class ResultGetChildMenusForUserActivityDto
    {
        public long Id { get; set; }
        public string ParentName { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public DateTime InsertTime { get; set; }
        public List<ResultGetChildMenus_CategoryForUserActivityDto> ResultCategories { get; set; }
        public List<ResultGetChildMenus_TagsForUserActivityDto> ResultTags { get; set; }
    }
    public class ResultGetChildMenus_CategoryForUserActivityDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
    public class ResultGetChildMenus_TagsForUserActivityDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
    #endregion
}

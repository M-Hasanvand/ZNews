using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.Menus.Queries.GetMenusForUserActivity
{
    public interface IGetMenusForUserActivityService
    {
        ResultDto<List<ResultGetMenusForUserActivityDto>> Execute(long UserId);
    }
    public class GetMenusForUserActivityService : IGetMenusForUserActivityService
    {
        private readonly IDataBaseContext _context;
        public GetMenusForUserActivityService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetMenusForUserActivityDto>> Execute(long UserId)
        {
            if (UserId == 0)
            {
                return new ResultDto<List<ResultGetMenusForUserActivityDto>>()
                {
                    IsSuccess = false,
                    Message = "ایدی ادمین ارسال نشد"
                };
            }
            var menus = _context.Menus.Include(c => c.Children).Where(p=>p.UserId==UserId).Select(p => new ResultGetMenusForUserActivityDto()
            {
                Id = p.Id,
                Name = p.Name,
                IsActive = p.IsActive,
                InsertTime=p.InsertTime,
                ChildCount = _context.ChildMenus.Where(ch => ch.ParentId == p.Id).Count()
            }).OrderByDescending(p => p.InsertTime).ToList();
            if (menus.Count == 0)
            {
                return new ResultDto<List<ResultGetMenusForUserActivityDto>>()
                {
                    IsSuccess=false,
                    Message = " منویی ثبت نشده  "
                };
            }
            return new ResultDto<List<ResultGetMenusForUserActivityDto>>()
            {
                Data=menus,
                IsSuccess=true
            };
        }
    }

    public class ResultGetMenusForUserActivityDto
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public DateTime InsertTime { get; set; }
        public long ChildCount { get; set; }
    }
}

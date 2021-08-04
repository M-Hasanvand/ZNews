using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.News.Queries.GetNewsForUserActivity
{
    public interface IGetNewsForUserActivityService
    {
        ResultDto<List<ResultGetNewsForUserActivityDto>> Execute(long UserId);
    }
    public class GetNewsForUserActivityService:IGetNewsForUserActivityService
    {
        private readonly IDataBaseContext _context;
        public GetNewsForUserActivityService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultGetNewsForUserActivityDto>> Execute(long UserId)
        {
            if (UserId == 0)
            {
                return new ResultDto<List<ResultGetNewsForUserActivityDto>>()
                {
                    IsSuccess = false,
                    Message = "ایدی ادمین ارسال نشد"
                };
            }
            var news = _context.News.Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.NewsInTages)
                .ThenInclude(p => p.Tag)
                .Where(p=>p.UserId==UserId).Select(p=>new ResultGetNewsForUserActivityDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Views = p.Views,
                    IsActive = p.IsActive,
                    CategoryName = p.Category.Name,
                    InsertTime = p.InsertTime,
                    IsSlider = p.IsSlider,
                    CommentsNumber = _context.Comments.Where(c=>c.NewsId==p.Id).Count(),
                    Tags = p.NewsInTages.Where(n => n.NewsId == p.Id).Select(t => new TagsGetNewsForUserActivityDto()
                    {
                        Name = t.Tag.Name
                    }).ToList(),
                    UserGuid = p.User.UserGuid,
                    UserId = p.User.Id,
                }).OrderByDescending(p=>p.InsertTime).ToList();
            if (news.Count == 0)
            {
                return new ResultDto<List<ResultGetNewsForUserActivityDto>>()
                {
                    IsSuccess = false,
                    Message = " خبری ثبت نشده"
                };
            }
            return new ResultDto<List<ResultGetNewsForUserActivityDto>>()
            {
                Data= news,
                IsSuccess=true
            };
        }
    }
    #region [NewsDto]
    public class ResultGetNewsForUserActivityDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public DateTime InsertTime { get; set; }
        public long Views { get; set; }
        public long CommentsNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsSlider { get; set; }
        public Guid UserGuid { get; set; }
        public long UserId { get; set; }
        public virtual List<TagsGetNewsForUserActivityDto> Tags { get; set; }

    }
    public class TagsGetNewsForUserActivityDto
    {
        public string Name { get; set; }
    }
    #endregion
}

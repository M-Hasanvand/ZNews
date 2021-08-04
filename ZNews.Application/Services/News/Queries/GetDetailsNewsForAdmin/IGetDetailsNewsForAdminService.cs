using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.News.Queries.GetDetailsNewsForAdmin
{
    public interface IGetDetailsNewsForAdminService
    {
        ResultDto<ResultDetailsNewsForAdminDto> Execute(long NewsId);
    }
    public class GetDetailsNewsForAdminService : IGetDetailsNewsForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetDetailsNewsForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultDetailsNewsForAdminDto> Execute(long NewsId)
        {
            var news = _context.News.Include(p=>p.User)
                .Include(p=>p.Category)
                .Include(p=>p.NewsInTages)
                .ThenInclude(p=>p.Tag)
                .Where(p=>p.Id==NewsId)
                .FirstOrDefault();
            if (news==null)
            {
                return new ResultDto<ResultDetailsNewsForAdminDto>()
                {
                    Message="خبر وجود ندارد",
                    IsSuccess = false,
                };
            }
            return new ResultDto<ResultDetailsNewsForAdminDto>()
            {
                Data = new ResultDetailsNewsForAdminDto()
                {
                    Id=news.Id,
                    Title = news.Title,
                    Description = news.Description,
                    ShortDescription = news.ShortDescription,
                    IsActive = news.IsActive,
                    ImageUrl = news.ImageUrl,
                    CategoryId = news.CategoryId,
                    CategoryName=news.Category.Name,
                    UserId = news.UserId,
                    UserName=news.User.FullName,
                    InsertTime=news.InsertTime,
                    CommentCount= commentCount(news.Id),
                    Tags =news.NewsInTages.Where(p=>p.NewsId==news.Id).Select(p=>new TagsGetDeatilsNewsForAdminDto()
                    {
                        Id=p.Tag.Id,
                        Name=p.Tag.Name
                    }).ToList()
                },
                IsSuccess = true,
            };
        }
        private long commentCount(long NewsId)
        {
            return _context.Comments.Where(p => p.NewsId == NewsId).Count();
        }
    }
    public class ResultDetailsNewsForAdminDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public long Views { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime InsertTime { get; set; }
        public long CommentCount { get; set; }
        public virtual List<TagsGetDeatilsNewsForAdminDto> Tags { get; set; }
    }
    public class TagsGetDeatilsNewsForAdminDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;
using ZNews.Common.Extensions;
using ZNews.Domain.Entities.Newses;

namespace ZNews.Application.Services.News.Queries.GetDailyNewsListForSite
{
    public interface IGetDailyNewsListForSiteService
    {
        ResultDto<List<ResultGetNewsForSiteDto>> Execute();
    }
    public class GetDailyNewsListForSiteService : IGetDailyNewsListForSiteService
    {
        private readonly IDataBaseContext _context;
        public GetDailyNewsListForSiteService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultGetNewsForSiteDto>> Execute()
        {
            var news= _context.News.Include(p=>p.NewsInTages).Where(p=>p.IsActive==true&&p.IsSlider==false).Select(p => new ResultGetNewsForSiteDto()
            {
                Id = p.Id,
                Title = p.Title,
                ShortDescription=p.ShortDescription,
                CategoryId=p.CategoryId,
                CategoryName = p.Category.Name,
                UserName =p.User.FullName,
                InsertTime = p.InsertTime,
                CoverNews=p.ImageUrl,
            }).OrderByDescending(p => p.InsertTime).ToList();
            return new ResultDto<List<ResultGetNewsForSiteDto>>()
            {
                Data = news,
                IsSuccess = true,
            };
        }
    }
    public class ResultGetNewsForSiteDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string CategoryName { get; set; }
        public long CategoryId { get; set; }

        public string UserName { get; set; }
        public string CoverNews { get; set; }
        public DateTime InsertTime { get; set; }
    }
}

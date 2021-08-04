
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

namespace ZNews.Application.Services.News.Queries.GetNewsForSliderSite
{
    public interface IGetNewsForSliderSiteService
    {
        ResultDto<List<ResultGetNewsForSliderSiteDto>> Execute();
    }
    public class GetNewsForSliderSiteService : IGetNewsForSliderSiteService
    {
        private readonly IDataBaseContext _context;
        public GetNewsForSliderSiteService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultGetNewsForSliderSiteDto>> Execute()
        {
            var news = _context.News.Include(p => p.NewsInTages).Where(p => p.IsActive == true && p.IsSlider == true).Select(p => new ResultGetNewsForSliderSiteDto()
            {
                Id = p.Id,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                CategoryId=p.CategoryId,
                CategoryName = p.Category.Name,
                UserName = p.User.FullName,
                InsertTime = p.InsertTime,
                CoverNews = p.ImageUrl,
            }).OrderByDescending(p => p.InsertTime).ToList();
            return new ResultDto<List<ResultGetNewsForSliderSiteDto>>()
            {
                Data = news,
                IsSuccess = true,
            };
        }
    }
    public class ResultGetNewsForSliderSiteDto
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

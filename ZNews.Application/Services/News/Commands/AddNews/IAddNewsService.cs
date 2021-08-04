using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;
using ZNews.Common.Extensions;
using ZNews.Domain.Entities.Newses;

namespace ZNews.Application.Services.News.Commands.AddNews
{
    public interface IAddNewsService
    {
        ResultDto Execute(RequestAddNewsDto request);
    }
    public class AddNewsService: IAddNewsService
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public AddNewsService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public ResultDto Execute(RequestAddNewsDto request)
        {
            var category = _context.Categories.Find(request.CategoryId);
            var user = _context.Users.Find(request.UserId);
            Domain.Entities.Newses.News news = new Domain.Entities.Newses.News()
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Description = request.Description,
                VideoUrl = request.VideoUrl,
                IsActive =true,
                CategoryId=request.CategoryId,
                Category= category,
                UserId =request.UserId,
                User= user
            };
            if (request.ImageUrl!=null)
            {
                news.ImageUrl = Extension.UploadFile(request.ImageUrl, @"Images\News\Covers\", _environment);
            }
            List<NewsInTag> tags = new List<NewsInTag>();
            foreach (var item in request.Tags)
            {
                var tag = _context.Tags.Find(item.TagId);
                tags.Add(new NewsInTag() 
                { 
                    TagId=tag.Id,
                    NewsId=news.Id,
                    News=news,
                    Tag=tag,
                });
            }
            _context.NewsInTags.AddRange(tags);
            _context.News.Add(news);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "خبر جدید ثبت شد"
            };
        }
    }
    public class RequestAddNewsDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public IFormFile ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public long CategoryId { get; set; }
        public long UserId { get; set; }
        public virtual List<RequestTagAddNewsDto> Tags { get; set; }
    }
    public class RequestTagAddNewsDto
    {
        public long TagId { get; set; }
    }
}

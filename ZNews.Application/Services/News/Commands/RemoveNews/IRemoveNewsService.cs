using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;
using ZNews.Common.Extensions;

namespace ZNews.Application.Services.News.Commands.RemoveNews
{
    public interface IRemoveNewsService
    {
        ResultDto Execute(long NewsId);
    }
    public class RemoveNewsService : IRemoveNewsService
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public RemoveNewsService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public ResultDto Execute(long NewsId)
        {
            var news = _context.News.Find(NewsId);
            if (news == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خبر مورد نظر یافت نشد"
                };
            }
            var comments = _context.Comments.Where(p => p.NewsId == news.Id).ToList();
            foreach (var itemComment in comments)
            {
                itemComment.RemoveTime = DateTime.Now;
                itemComment.IsRemove = true;
            }
            var Tags = _context.NewsInTags.Where(p => p.NewsId == news.Id).ToList();
            foreach (var itemTag in Tags)
            {
                itemTag.RemoveTime = DateTime.Now;
                itemTag.IsRemove = true;
            }
            news.RemoveTime = DateTime.Now;
            news.IsRemove = true;
            if (!string.IsNullOrWhiteSpace(news.ImageUrl))
            {
                news.ImageUrl = Extension.MoveFile(news.ImageUrl, @"RecycleBin\News\Cover\", _environment);
            }
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "خبر مورد نظر حذف شد",
            };
        }
    }
}

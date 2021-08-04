using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;

namespace ZNews.Application.Services.News.Commands.EditNews
{
    public interface IEditNewsService
    {
        ResultDto Execute(RequestEditNewsDto request);
    }
    public class EditNewsService: IEditNewsService
    {
        private readonly IDataBaseContext _context;
        public EditNewsService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestEditNewsDto request)
        {
            var news = _context.News.Include(p => p.NewsInTages).ThenInclude(p => p.Tag).Where(p => p.Id == request.Id).FirstOrDefault();
            if (news == null)
            {
                return new ResultDto()
                {
                    Message = "خبر وجود ندارد",
                    IsSuccess = false,
                };
            }
            news.Title= request.Title;
            news.ShortDescription = request.ShortDescription;
            news.Description= request.Title;
            news.ImageUrl=request.ImageUrl;
            news.CategoryId=request.CategoryId;
            news.UpdateTime = DateTime.Now;
            _context.SaveChanges();
            return new ResultDto()
            {
                Message="ویرایش خبر انجام شد",
                IsSuccess = true,
            };
        }
    }
    public class RequestEditNewsDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public long CategoryId { get; set; }
        public virtual List<TagsGetDeatilsNewsForAdminDto> Tags { get; set; }
    }
    public class TagsGetDeatilsNewsForAdminDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}

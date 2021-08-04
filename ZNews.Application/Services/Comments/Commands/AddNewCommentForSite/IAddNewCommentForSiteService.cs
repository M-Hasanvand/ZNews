using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;
using ZNews.Domain.Entities.Newses;

namespace ZNews.Application.Services.Comments.Commands.AddNewCommentForSite
{
    public interface IAddNewCommentForSiteService
    {
        ResultDto Execute(RequestAddNewCommentForSiteDto request);
    }
    public class AddNewCommentForSiteService : IAddNewCommentForSiteService
    {
        private readonly IDataBaseContext _context;
        public AddNewCommentForSiteService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddNewCommentForSiteDto request)
        {
            var news = _context.News.Find(request.NewsId);
            if (news == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خبر مورد نظر موجو نیست"
                };
            }
            if (string.IsNullOrWhiteSpace(request.FullName))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "نام  خود را وارد کنید"
                };
            }
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "ایمیل خود را وارد کنید"
                };
            }


            if (string.IsNullOrWhiteSpace(request.Text))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "متن خود را وارد کنید"
                };
            }
            Comment comment = new Comment()
            {
                News = news,
                NewsId = news.Id,
                ParentId = request.ParentId,
                IsActive = false,
                Email = request.Email,
                FullName = request.FullName,
                Text = request.Text,
            };
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"{request.FullName} نظر شما بعد از تایید ادمین در سایت منتشر میشود "
            };
        }
    }
    public class RequestAddNewCommentForSiteDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public long? ParentId { get; set; }
        public long NewsId { get; set; }
    }
}

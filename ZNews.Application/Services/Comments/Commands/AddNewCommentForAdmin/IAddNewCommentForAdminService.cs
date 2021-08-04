using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Common.Dto;
using ZNews.Domain.Entities.Newses;

namespace ZNews.Application.Services.Comments.Commands.AddNewCommentForAdmin
{
    public interface IAddNewCommentForAdminService
    {
        ResultDto Execute(RequestAddNewCommentForAdminDto request);
    }
    public class AddNewCommentForAdminService : IAddNewCommentForAdminService
    {
        private readonly IDataBaseContext _context;
        public AddNewCommentForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddNewCommentForAdminDto request)
        {
            var news = _context.News.Find(request.NewsId);
            var user = _context.Users.Find(request.UserId);
            if (news == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خبر مورد نظر موجو نیست"
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
                UserId = user.Id,
                User = user,
                IsActive = false,
                Email =user.Email,
                FullName = user.FullName,
                Text = request.Text,
            };
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"{user.FullName} نظر شما ثبت شد"
            };
        }
    }
    public class RequestAddNewCommentForAdminDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public long UserId { get; set; }
        public long? ParentId { get; set; }
        public long NewsId { get; set; }
    }
}
